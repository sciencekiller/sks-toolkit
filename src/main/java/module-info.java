module com.skstoolkit.skstoolkit {
    requires javafx.controls;
    requires javafx.fxml;
    requires com.jfoenix;
    requires com.fasterxml.jackson.databind;
    requires com.fasterxml.jackson.core;
    requires org.apache.commons.codec;
    requires org.apache.httpcomponents.httpcore;
    requires org.apache.httpcomponents.httpclient;


    opens com.skstoolkit.skstoolkit to javafx.fxml;
    exports com.skstoolkit.skstoolkit;
}