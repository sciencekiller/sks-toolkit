//包声明
package com.skstoolkit.skstoolkit;

//导入包

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import javafx.application.Application;
import org.slf4j.*;
import java.io.IOException;
import java.io.InputStream;

public class Main{
    public static InputStream jsonStream=null;
    public static String version="";
    public static String build="";
    public static String channel="";
    public static void initialize() {
        jsonStream = Main.class.getClassLoader().getResourceAsStream("version.json");//输入流获取json
        ObjectMapper mapper = new ObjectMapper();
        JsonNode rootNode = null;
        try {
            rootNode = mapper.readTree(jsonStream);
        } catch (IOException e) {
            e.printStackTrace();
        }
        assert rootNode != null;
        version = rootNode.path("version").asText();
        build = rootNode.path("build").asText();
        channel = rootNode.path("channel").asText();
    }

    public static void main(String[] args) {
        initialize();
        Application.launch(Window_UI.class,args);
    }
}
