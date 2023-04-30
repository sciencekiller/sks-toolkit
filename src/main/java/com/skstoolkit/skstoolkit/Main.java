//包声明
package com.skstoolkit.skstoolkit;

//导入包

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import javafx.application.Application;

import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;

public class Main{
    public static InputStream jsonStream=null;
    public static String version="";
    public static String build="";
    public static String channel="";
    public static String check_url="";
    public static String Temp_Folder="";
    public static String latest_version="";
    public static void initialize() {
        jsonStream = Main.class.getClassLoader().getResourceAsStream("config.json");//输入流获取json
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
        check_url = rootNode.path("check_url").asText();
        Temp_Folder=System.getenv("TEMP");
        String result=Methods.sendGetRequest(check_url);
        JsonNode update_node=null;
        if(result!=null){
            try{
                update_node=mapper.readTree(result);
            }catch(IOException e){
                e.printStackTrace();
            }
        }
        assert update_node != null;
        latest_version=update_node.path("tag_name").asText();
        System.out.println(latest_version);
    }

    public static void main(String[] args) {
        initialize();
        Application.launch(Window_UI.class,args);
    }
}
