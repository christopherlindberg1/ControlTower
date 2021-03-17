# Control Tower
This app is part of a C# course I've taken on Malmö University. The app is used for a control tower to keep track of different actions that airplanes do (e.g. take off, change heading).

## Learning goals
There were two main learning goals with this project:
* Learn WPF
* Learn the publisher-subscriber pattern and work with delegates and events.

## Features
As compared to the real world, the only responsability of the Control tower is to send airplanes to the runway. Ones that is done, the airplane themselves decide when to take off, change heading and land. The "airplanes" have events for these actions, which the control tower is listening to. Once an event is triggered the control tower logs the information in a list. This list is empty every time the app is started.

The app also logs the date and time for all take offs and landings in an XML-file so that the user can access all historical logs and not just this session. The user can then view the full log and filter by flight code and datetime.