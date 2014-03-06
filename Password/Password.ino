#include <EEPROM.h>
  int addr = 0;
int incomingByte = 0;   // for incoming serial data
int array[7];
int i=0;
char pwd[20]="operatingsystems";
void setup() {
      
       for(int i=0;i<20;i++)  
    {
     EEPROM.write(i,pwd[i]); 
      
    }
  
        Serial.begin(9600);     // opens serial port, sets data rate to 9600 bps
}

void loop() {
  
  
  
          
        // send data only when you receive data:
        if (Serial.available() > 0)
        {
                // read the incoming byte:
                incomingByte = Serial.read();
                array[i]=incomingByte;   
              //Serial.write(incomingByte);
            
            i++   ; 
        }
                
        if(i==7)
         {  
           
           i=0; 
            if(array[0]=='S')
            {  
              if(array[1]=='W')
                {  if(array[5]=='E')
                  {  if(array[6]=='P')
                    {                      
                      int addr = (int)array[3] ;                      
                      EEPROM.write(addr, array[4]);
                      Serial.write(EEPROM.read(addr));
                    }
                  }
                }
                else if(array[1]=='R')
                {  if(array[5]=='E')
                  {  if(array[6]=='P')
                    {                      
                      uint16_t addr = (uint16_t)array[3];
                      
                      Serial.write(EEPROM.read(addr));
                                  
                    }
                  }
                }
            }  
            

         }           
                
                
          


  
                //delay(1000); 
                //Serial.write("xyz");

                // say what you got:
                
               
                
                
                
                
                
        }

 
