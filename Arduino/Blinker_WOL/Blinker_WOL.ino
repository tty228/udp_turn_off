#define BLINKER_WIFI
#define BLINKER_MIOT_OUTLET

#include <Blinker.h>
#include <WiFiUdp.h>
#include <WiFi.h>

//用户自定义变量，将***替换
char auth[] = "*********"; //密钥
char ssid[] = "*********"; //wifi名
char pswd[] = "*********"; //wifi密码
char * ip = "***.***.***.***";//电脑ip地址，如需要群发最后为.255
byte mac[] = {0x**,0x**,0x**,0x**,0x**,0x**};//唤醒目标电脑的mac
char pac[102];
byte preamble[] = {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};//唤醒包包头数据，无需更改
int Port = 8080;//udp 广播端口，一般无需更改，除非端口被占用
char *my_data_to_send = "turn_off_the_computer";//C#程序监听的关机指令

WiFiUDP UDP;  //建立一个WiFiUDP对象 UDP
bool oState = false;//设置初始状态为关机

// 新建按钮事件
BlinkerButton Button1("btn-abc");

void button1_callback(const String & state) {
  BLINKER_LOG("get button state: ", state);
  if (state == BLINKER_CMD_ON) {
    pcawaking();//唤醒电脑
  } else if (state == BLINKER_CMD_OFF) {
    pcclose();//关闭电脑
  }
}

void pcawaking()
{
    UDP.beginPacket(ip, Port); //UDP 发送到目标（IP，端口）
    UDP.write(preamble, sizeof preamble); //写入包头(FF,FF,FF,FF,FF,FF)
    for (byte i = 0; i < 16; i++)
    {
      UDP.write(mac, sizeof mac);
    }
    UDP.endPacket();//发送 UDP 广播
    digitalWrite(LED_BUILTIN, HIGH);
    BLINKER_LOG("电脑开了!");
    Button1.color("#FF0000");//设置app按键是红色
    Button1.text("关机");
    Button1.print("on");
    BlinkerMIOT.powerState("on");
    BlinkerMIOT.print();
    oState = true;
}

void pcclose()
{
    UDP.beginPacket(ip, Port);
    for (int i = 0; i < strlen(my_data_to_send); i++)
    {
        UDP.write((uint8_t)my_data_to_send[i]);
    }
    UDP.endPacket();//发送 UDP 广播
    digitalWrite(LED_BUILTIN, LOW);
    BLINKER_LOG("电脑关了!");
    Button1.color("#00BB00");//设置app按键是绿色
    Button1.text("开机");
    Button1.print("off");
    BlinkerMIOT.powerState("off");
    BlinkerMIOT.print();
    oState = false;
}

//用户自定义电源类操作的回调函数:
void miotPowerState(const String & state)
{
    BLINKER_LOG("need set power state: ", state);

    if (state == BLINKER_CMD_ON) {
        pcawaking();//唤醒电脑
    }
    else if (state == BLINKER_CMD_OFF) {
        pcclose();//关闭电脑
    }
}

//小爱同学查询接口
void miotQuery(int32_t queryCode)
{
    BlinkerMIOT.powerState(oState ? "on" : "off");
    BlinkerMIOT.print();
}

// 初始化
void setup()
{
    // 初始化串口，并开启调试信息，调试用可以删除
    Serial.begin(115200);
    BLINKER_DEBUG.stream(Serial);

     // 初始化blinker
    Blinker.begin(auth, ssid, pswd);
    Button1.attach(button1_callback);

    //小爱同学回调函数
    BlinkerMIOT.attachPowerState(miotPowerState);//注册回调函数
    BlinkerMIOT.attachQuery(miotQuery);    //语音指令与控制函数对应(查询指令)
}

void loop()
{
    Blinker.run();
   //Blinker.run()语句负责处理Blinker收到的数据，每次运行都会将设备收到的数据进行一次解析。
   //在使用WiFi接入时，该语句也负责保持网络连接
}
