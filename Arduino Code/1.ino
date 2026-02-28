#include <Wire.h>


#define R 303.0
#define Vs 10

#define analogPin     A3         // analog pin for measuring capacitor voltage
#define chargePin      13         // pin to charge the capacitor - connected to one end of the charging resistor
#define dischargePin   11         // pin to discharge the capacitor
#define resistorValue  10000.0F   // change this to whatever resistor value you are using
                                   // F formatter tells compliler it's a floating point value
unsigned long startTime;
unsigned long elapsedTime;
float microFarads;                // floating point variable to preserve precision, make calculations
float nanoFarads;
char win;

void setup() {
  pinMode(chargePin, OUTPUT);     // set chargePin to output
   digitalWrite(chargePin, LOW);
  cli();
  // put your setup code here, to run once:
  Serial.begin(38400);
  //Timer0 enable with 2.048ms OVF priode
  TCCR2B=(0<<WGM22) | (1<<CS22) | (0<<CS21) | (1<<CS20);
  // Timer/Counter 2 Interrupt(s) initialization
  TIMSK2=(0<<OCIE2B) | (0<<OCIE2A) | (1<<TOIE2);
  //Global Intrrupt enable
  sei();
  //Reset Timer0 Value
  TCNT2=0;
  
  Wire.begin(8);                // join i2c bus with address #8
  Wire.onReceive(receiveEvent); // register event
  
}


void loop()
{
  
}

ISR(TIMER2_OVF_vect){
  char pocket[30];
  unsigned int v0=0;
  float v1=0,i,vr;
  unsigned int v2=0;
  unsigned int power=0;
  unsigned int Rin=0;
  

  v0 = analogRead(0)*5;
  v1 = analogRead(1)*5;
  v2 = analogRead(2)*5;
  
  v0=v0/10.23;
  
  v1=v1/511; 
  i=(Vs-v1)*1000/R;
  power=i*v1; //power by miliamper
  //v0 = map(v0, 0, 500, 0, 110);
  //power = map(power, 0, 200, 0, 110);

  vr = v2/1023.00;

  Rin=(100*v0/vr)-10000;
  //Serial.println(rin);
  //Rin = map(power, 0, 10000, 0, 100);
  sprintf(pocket,"%d|%d|%d\n",v0,power,Rin);
  //Serial.print(pocket);
}


void receiveEvent(int howMany) {
  while (1 < Wire.available()) { // loop through all but the last
    win = Wire.read(); // receive byte as a character
    Serial.println(win);
             // print the character 
  }        // print the integer
}
