package com.manning.apisecurityinaction;

import com.google.common.util.concurrent.RateLimiter;
import com.manning.apisecurityinaction.controller.*;
import com.manning.apisecurityinaction.token.*;
import org.dalesbred.Database;
import org.dalesbred.result.EmptyResultException;
import org.h2.jdbcx.JdbcConnectionPool;
import org.json.*;
import spark.*;

import java.io.FileInputStream;
import java.net.URI;
import java.nio.file.*;
import java.security.KeyStore;
import java.util.Set;

import static spark.Service.SPARK_DEFAULT_PORT;
import static spark.Spark.*;

public class Main {

    public static void main(String... args) throws Exception {
        Spark.staticFiles.location("/public");
        secure("localhost.p12", "changeit", null, null);
        port(args.length > 0 ? Integer.parseInt(args[0])
                             : SPARK_DEFAULT_PORT);

        var datasource = JdbcConnectionPool.create(
            "jdbc:h2:mem:natter", "natter", "password");
        var database = Database.forDataSource(datasource);
        createTables(database);
        datasource = JdbcConnectionPool.create(
            "jdbc:h2:mem:natter", "natter_api_user", "password");

        database = Database.forDataSource(datasource);
        var spaceController = new SpaceController(database);
        var userController = new UserController(database);
        var auditController = new AuditController(database);

        var rateLimiter = RateLimiter.create(2.0d);
        before((request, response) -> {
            if (!rateLimiter.tryAcquire()) {
                halt(429);
            }
        });
        before(new CorsFilter(Set.of("https://localhost:9999")));

        before(((request, response) -> {
            if (request.requestMethod().equals("POST") &&
            !"application/json".equals(request.contentType())) {
                halt(415, new JSONObject().put(
                    "error", "Only application/json supported"
                ).toString());
            }
        }));

        afterAfter((request, response) -> {
            response.type("application/json;charset=utf-8");
            response.header("X-Content-Type-Options", "nosniff");
            response.header("X-Frame-Options", "DENY");
            response.header("X-XSS-Protection", "0");
            response.header("Cache-Control", "no-store");
            response.header("Content-Security-Policy",
                    "default-src 'none'; frame-ancestors 'none'; sandbox");
            response.header("Server", "");
        });

        var keyPassword = System.getProperty("keystore.password",
                "changeit").toCharArray();
        var keyStore = KeyStore.getInstance("PKCS12");
        keyStore.load(new FileInputStream("keystore.p12"),
                keyPassword);
        var macKey = keyStore.getKey("hmac-key", keyPassword);
        var encKey = keyStore.getKey("aes-key", keyPassword);

        var clientId = "testClient";
        var clientSecret = "60ho9IS3d6/A+Zzvdn9Y4laiGnI/1TddTM95lEHjArw=";
        var introspectionEndpoint =
                URI.create("https://as.example.com:8443/oauth2/introspect");
        SecureTokenStore tokenStore = HmacTokenStore.wrap(
                new DatabaseTokenStore(database), macKey);
        var tokenController = new TokenController(tokenStore);

        before(userController::authenticate);
        before(tokenController::validateToken);

        before(auditController::auditRequestStart);
        afterAfter(auditController::auditRequestEnd);

        var droolsController = new DroolsAccessController();
        before("/*", droolsController::enforcePolicy);

        before("/sessions", userController::requireAuthentication);
        before("/sessions",
                tokenController.requireScope("POST", "full_access"));
        post("/sessions", tokenController::login);
        delete("/sessions", tokenController::logout);

        before("/spaces", userController::requireAuthentication);
        before("/spaces",
                tokenController.requireScope("POST", "create_space"));
        post("/spaces", spaceController::createSpace);

        // Additional REST endpoints not covered in the book:
        before("/spaces/:spaceId/messages", userController::lookupPermissions);
        before("/spaces/:spaceId/messages/*", userController::lookupPermissions);
        before("/spaces/:spaceId/members", userController::lookupPermissions);

        before("/spaces/*/messages",
                tokenController.requireScope("POST", "post_message"));
        before("/spaces/:spaceId/messages",
                userController.requirePermission("POST", "w"));
        post("/spaces/:spaceId/messages", spaceController::postMessage);

        before("/spaces/*/messages/*",
                tokenController.requireScope("GET", "read_message"));
        before("/spaces/:spaceId/messages/*",
                userController.requirePermission("GET", "r"));
        get("/spaces/:spaceId/messages/:msgId",
            spaceController::readMessage);

        before("/spaces/*/messages",
                tokenController.requireScope("GET", "list_messages"));
        before("/spaces/:spaceId/messages",
                userController.requirePermission("GET", "r"));
        get("/spaces/:spaceId/messages", spaceController::findMessages);

        before("/spaces/*/members",
                tokenController.requireScope("POST", "add_member"));
        before("/spaces/:spaceId/members",
                userController.requirePermission("POST", "rwd"));
        post("/spaces/:spaceId/members", spaceController::addMember);

        var moderatorController =
            new ModeratorController(database);

        before("/spaces/*/messages/*",
                tokenController.requireScope("DELETE", "delete_message"));
        before("/spaces/:spaceId/messages/*",
                userController.requirePermission("DELETE", "d"));
        delete("/spaces/:spaceId/messages/:msgId",
            moderatorController::deletePost);

        get("/logs", auditController::readAuditLog);
        post("/users", userController::registerUser);

        internalServerError(new JSONObject()
            .put("error", "internal server error").toString());
        notFound(new JSONObject()
            .put("error", "not found").toString());

        exception(IllegalArgumentException.class, Main::badRequest);
        exception(JSONException.class, Main::badRequest);
        exception(EmptyResultException.class,
            (e, request, response) -> response.status(404));
    }

  private static void badRequest(Exception ex,
      Request request, Response response) {
    response.status(400);
    response.body(new JSONObject().put("error", ex.getMessage()).toString());
  }

    private static void createTables(Database database) throws Exception {
        var path = Paths.get(
                Main.class.getResource("/schema.sql").toURI());
        database.update(Files.readString(path));
    }
}