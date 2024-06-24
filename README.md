Water SCADA Desktop Application

A comprehensive SCADA (Supervisory Control and Data Acquisition) system developed using WPF, XAML, and C#. This application is designed to monitor and control various components in water management systems, such as pumps, pumping stations, reservoirs, and more.

Table of Contents

    Introduction
    Features
    Technologies Used 
    Screenshots
    Architecture and Design
   
   

Introduction

The Water SCADA Desktop Application is designed to provide real-time monitoring and control of various components in a water management system. This includes pumps, pumping stations, reservoirs, water levels, and more. The application offers detailed reporting and charting capabilities to ensure efficient water management and system monitoring.
Features

    Pump and Pumping Stations Monitoring: Real-time monitoring and control of pumps and pumping stations.
    Reservoir Management: Track and manage water levels in reservoirs.
    Bimetal Control: Monitor and control bimetal components.
    Phase Control: Manage and monitor phase control elements.
    RTU Status: Real-time updates on Remote Terminal Unit (RTU) status.
    Battery Monitoring: Keep track of battery status and health.
    Logging: Comprehensive logging of system events and statuses.
    Reports and Charts: Generate detailed reports and charts for system analysis and performance tracking.

Technologies Used

    WPF (Windows Presentation Foundation)
    XAML (Extensible Application Markup Language)
    C#
    MVVM (Model-View-ViewModel) Architecture

Screenshots

![waterscada](https://github.com/YaldaAHM/Water_SCADA/assets/169922419/5b135a2a-45da-4585-908e-8ee2b39cf5aa)

Architecture and Design

The application is built using the MVVM architectural pattern, which promotes a clean separation of concerns. This makes the application more modular, testable, and maintainable.
Components

    Model: Represents the application's data and business logic.
    View: Defines the user interface, developed using XAML.
    ViewModel: Acts as an intermediary between the View and the Model, handling user input and updating the View accordingly.
