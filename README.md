# Arduino Data Logger

This project is a comprehensive data logging and visualization system that uses an Arduino to collect analog sensor data and a custom C# Windows Forms application to display the data in real-time.

## Project Structure

The project is divided into two main components:

1. **Arduino Firmware** (`Arduino Code/`)
   - Reads analog signals from sensors (A0, A1, A2).
   - Calculates Voltage, Power, and Resistance based on sensor inputs.
   - Transmits processed data via Serial communication.
   - Includes I2C slave functionality for external control/communication.

2. **Desktop Application** (`Application/`)
   - A C# Windows Forms application named "analizer".
   - Connects to the Arduino via Serial Port.
   - Receives data streams (Voltage | Power | Resistance).
   - Visualizes the data on a real-time graph.

## Arduino Firmware Details

The firmware (`1.ino`) is designed to:
- Sample analog inputs A0, A1, and A2.
- Perform calculations:
    - **Current (`i`)**: Calculated based on the voltage drop across a shunt resistor (R = 303.0Ω).
    - **Power**: Calculated as Current × Voltage.
    - **Resistance (`Rin`)**: Derived from voltage ratios.
- Output formatted strings to the serial port in the format: `value1|value2|value3\n`.
- Handle I2C events (Address #8).

## Desktop Application Details

The C# application serves as the dashboard for the data logger:
- **Visual Interface**: Utilizes a `PictureBox` to draw real-time waveforms.
- **Data Parsing**: Splits incoming serial strings using the `|` delimiter.
- **Scaling**: Applies specific scaling factors to fit values within the graph window.

## Usage

1. **Hardware Setup**:
   - Connect sensors to the Arduino Analog pins (A0, A1, A2).
   - Ensure the Reference Voltage (`Vs`) and Resistor values (`R`) in the code match your hardware.
   - Upload `1.ino` to your Arduino board.

2. **Software Setup**:
   - Open `Application/analizer.sln` in Visual Studio.
   - Build and Run the application.
   - The application will attempt to read from the defined serial port (ensure the correct port and baud rate `38400` are configured).
