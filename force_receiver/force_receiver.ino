namespace{
  int t = 128;
}

void delay_remodeled(float ms){
  delay(ms * 64.0 / pow(8, TCCR0B-1));
}

void setup() {
  pinMode(3, OUTPUT);
  TCCR0B = (TCCR0B & 0b11111000) | 0x01;
  Serial.begin(9600);
}

void loop() {
  Serial.println(t);    //Unity側で現在のduty比を取得可能にする
  if(Serial.available()){
    t = Serial.read();  //128 <= t <= 255
  }
  analogWrite(3, t);    //振動モータを駆動するためにPWMをドライバへ
  delay_tccr0b(1.0 / 60.0 * 1000.0);//60fpsで動作させる
}
