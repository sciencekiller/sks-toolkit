//包声明
package com.skstoolkit.skstoolkit;

//导入包
import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.layout.GridPane;
import javafx.stage.Stage;
import com.jfoenix.controls.*;
public class Window_UI extends Application {//继承Application
    public void start(Stage primaryStage) {
        //TabPane
        JFXTabPane pane = new JFXTabPane();
        pane.getStylesheets().add("Window_UI_Style.css");
        pane.setPrefHeight(400);
        pane.setPrefWidth(600);

        //Tabs

        //主页
        Tab MainTab = new Tab();
        MainTab.setText("主页");

        GridPane MainPane=new GridPane();
        Label Heading=new Label("Welcome to Sciencekill's toolkit");
        Heading.setId("Heading");
        Label version=new Label("您正在使用Sciencekill's Toolkit Version : "+Main.version);
        Label build=new Label("当前版本内部版本号 : "+Main.build);
        Label channel=new Label("当前渠道 : "+Main.channel);
        MainPane.add(Heading,0,0);
        MainPane.add(version,0,1);
        MainPane.add(build,0,2);
        MainPane.add(channel,0,3);
        MainTab.setContent(MainPane);

        //Messages Toolkit
        Tab MeTTab=new Tab();
        MeTTab.setText("消息批量发送");

        GridPane MeTPane=new GridPane();
        Label Met_Heading=new Label("Welcome to Messages Toolkit");
        Met_Heading.setId("Heading");
        Label MeT_Message_Label=new Label("发送内容:");

        MeTPane.add(Met_Heading,0,0);
        MeTPane.add(MeT_Message_Label,0,1);
        MeTTab.setContent(MeTPane);



        //Add tabs
        pane.getTabs().add(MainTab);
        pane.getTabs().add(MeTTab);

        //配置场景
        Scene primaryScene = new Scene(pane);

        //配置舞台
        primaryStage.setTitle("Sciencekill's Toolkit");//设置标题
        primaryStage.setScene(primaryScene);//配置场景
        primaryStage.show();//显示
    }
}
