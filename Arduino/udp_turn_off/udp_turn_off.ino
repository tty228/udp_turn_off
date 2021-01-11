#define BLINKER_WIFI                                           //官方 wifi 协议库
#define BLINKER_MIOT_OUTLET                                    //设置小爱插座类库
#define BLINKER_ALIGENIE_OUTLET                                //设置天猫插座类库
#define BLINKER_DUEROS_OUTLET                                  //设置小度插座类库

#include <Blinker.h>                                           //点灯科技
#include <WiFiUdp.h>                                           //udp 广播
#include <Ticker.h>                                            //定时器

// 用户自定义变量，将***替换
char auth[] = "*********";                                     //点灯科技密钥，需要使用阿里云通信，否则会提示无法连接
char ssid[] = "*********";                                     //wifi 名
char pswd[] = "*********";                                     //wifi 密码
byte mac[] = {0x**,0x**,0x**,0x**,0x**,0x**};                  //唤醒目标电脑的mac

// 初始化变量，一般无需更改
char pac[102];
char *btn_color;
char *btn_text;
char *btn_print;
char *power_status;
char packetBuffer[255];                                        //缓冲区来保存传入的数据包
char * ip = "255.255.255.255";                                 //udp 广播地址，如需要单播改为电脑 ip ，需要固定 ip 地址
int Port = 8080;                                               //udp 广播端口，一般无需更改，除非端口被占用
int localUdpPort = 2333;                                       //监听的本地端口
byte preamble[] = {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};        //唤醒包包头数据，无需更改
char *my_data_to_send = "turn_off_the_computer";               //C#程序监听的关机指令
char *power_status_to_send = "is_the_computer_on?";            //查询电脑开机状态
int count = 0;                                                 //初始化变量-检测关机是否成功的发包次数（因无法检测用户是否取消关机，通过每 10s 一个包检测通信状态）
bool oState = false;                                           //初始化开关状态为关

Ticker ticker;                                                 //声明 Ticker 对象
WiFiUDP UDP;                                                   //建立一个 WiFiUDP 对象 UDP
BlinkerButton Button1("btn-abc");                              //新建组件对象

// 按钮事件
void button1_callback(const String & state)
{
  if (btn_print=="off")
  {
    turn_on();                                                 //唤醒电脑
    awaking();                                                 //设置电源状态为开启
  } 
  else if (btn_print=="on")
  {
    turn_off();                                                //关闭电脑
    closed();                                                  //设置电源状态为关闭
  }
}

// 开电脑
void turn_on()
{
  UDP.beginPacket(ip, Port);                                   //udp 发送到目标（IP，端口）
  UDP.write(preamble, sizeof preamble);                        //写入包头(FF,FF,FF,FF,FF,FF)
  for (byte i = 0; i < 16; i++)
  {
    UDP.write(mac, sizeof mac);
  }
  UDP.endPacket();                                             //发送
}

// 设置电源状态为开启
void awaking()
{
  digitalWrite(LED_BUILTIN, LOW);                              //LED开
  power_status="电脑开着";
  btn_color="#FF0000";                                         //设置app按键是红色
  btn_text="点击关机";
  btn_print="on";
  oState = true;
}

// 关电脑
void turn_off()
{
  UDP.beginPacket(ip, Port);                                   //udp 发送到目标（IP，端口）
  for (int i = 0; i < strlen(my_data_to_send); i++)
  {
    UDP.write((uint8_t)my_data_to_send[i]);
  }
  UDP.endPacket();                                             //发送
}

// 设置电源状态为关闭
void closed()
{
  digitalWrite(LED_BUILTIN, HIGH);                             //LED关
  power_status="电脑关了";
  btn_color="#00BB00";                                         //设置app按键是绿色
  btn_text="点击开机";
  btn_print="off";
  oState = false;
}

// 发送电源状态查询指令
void status_query()
{
  closed();                                                    //先设置为状态关，因为懒不想做超时检测
  delay(2000);                                                 //等待 2s
  UDP.beginPacket(ip, Port);
  for (int i = 0; i < strlen(power_status_to_send); i++)
  {
    UDP.write((uint8_t)power_status_to_send[i]);
  }
  UDP.endPacket();                                             //发送
  ++count;
  if (count >= 10)
  {
    ticker.detach();                                           //关闭定时器
  }
}

// 电源类操作
void miotPowerState(const String & state)
{
  BLINKER_LOG("need set power state: ", state);
  if (state == BLINKER_CMD_ON)
  {
    turn_on();                                                 //唤醒电脑
    awaking();                                                 //设置电源状态为开启
  }
  else if (state == BLINKER_CMD_OFF)
  {
    turn_off();                                                //关闭电脑
    closed();                                                  //设置电源状态为关闭
  }
  heartbeat();
}

// 查询类操作
void Query()
{
  BlinkerMIOT.powerState(oState ? "on" : "off");
  BlinkerMIOT.print();
}

// 小爱同学电源类操作:
void PowerState()
{
  PowerState();
}

// 小爱同学查询接口
void miotQuery(int32_t queryCode)
{
  Query();
}

// 天猫精灵电源类操作:
void aligeniePowerState(const String & state)
{
  PowerState();
}

// 天猫精灵查询接口
void aligenieQuery(int32_t queryCode)
{
  Query();
}

// 小度语音电源类操作:
void duerPowerState(const String & state)
{
  PowerState();
}

// 小度语音查询接口
void duerQuery(int32_t queryCode)
{
  Query();
}

// APP 心跳包函数
void heartbeat()
{
  BLINKER_LOG(power_status);
  Button1.color(btn_color);                                    //设置app按键颜色
  Button1.text(btn_text);
  Button1.print(btn_print);
  BlinkerMIOT.powerState(btn_print);
  BlinkerMIOT.print();
}

// 设备初始化
void setup()
{
  // 初始化串口，并开启调试信息，调试用可以删除
  Serial.begin(115200);                                        //打开串行连接
  BLINKER_DEBUG.stream(Serial);                                //开启调试信息

  // 初始化blinker
  Blinker.begin(auth, ssid, pswd);                             //连接 wifi，连接点灯服务器
  Button1.attach(button1_callback);                            //初始化按钮事件

  //回调函数，用于反馈该控制状态
  BlinkerMIOT.attachPowerState(miotPowerState);                //小爱同学电源类操作回调函数
  BlinkerMIOT.attachQuery(miotQuery);                          //小爱同学电源类状态查询指令
  BlinkerAliGenie.attachPowerState(aligeniePowerState);        //天猫语音电源类操作回调函数
  BlinkerAliGenie.attachQuery(aligenieQuery);                  //天猫语音电源类状态查询指令
  BlinkerDuerOS.attachPowerState(duerPowerState);              //小度语音电源类操作回调函数
  BlinkerDuerOS.attachQuery(duerQuery);                        //小度语音电源类状态查询指令
  Blinker.attachHeartbeat(heartbeat);                          //app 定时向设备发送心跳包, 设备收到心跳包后会返回设备当前状态进行语音操作和app操作同步。
  delay(2000);                                                 //等待 2s
  UDP.begin(localUdpPort);                                     //启用UDP监听以接收数据
  status_query();                                              //发送电脑开机状态查询指令
}

// 循环
void loop()
{
  Blinker.run();                                               //负责处理Blinker收到的数据，每次运行都会将设备收到的数据进行一次解析。在使用WiFi接入时，该语句也负责保持网络连接

  int packetSize = UDP.parsePacket();                          //获取当前队首数据包长度
  if (packetSize)                                              //如果有数据可用
  {
    UDP.read(packetBuffer, 255);
    if (strcmp(packetBuffer, "the_computer_is_on") == 0)
    {
      awaking();                                                //设置电源状态为开启
    }
    else if(strcmp(packetBuffer, "the_computer_is_about_to_shut_down") == 0)
    {
      count = 0;
      ticker.attach(10, status_query);                          //启动定时器，每 10s 发送一次心跳包，检测是否取消关机
    }
    heartbeat();
    for (int i = 0; i < packetSize; i++)packetBuffer[i] = 0;    //清空缓存区，避免接收数据出错
  }
}
