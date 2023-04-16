module com.skstoolkit.skstoolkit {
    requires javafx.controls;
    requires javafx.fxml;
    requires com.jfoenix;
    requires com.fasterxml.jackson.databind;
    requires com.fasterxml.jackson.core;
    requires org.slf4j;


    opens com.skstoolkit.skstoolkit to javafx.fxml;
    exports com.skstoolkit.skstoolkit;
}