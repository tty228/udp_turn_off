# 小爱同学使用 esp32 网络唤醒电脑、关闭电脑

通过 esp32 单片机连接小爱同学（天猫精灵、小度未测试）
通过魔术包唤醒电脑开启，配合软件监听 UDP 广播进行关机操作
无需拆机，支持可以网络唤醒的笔记本

#### 主要功能
- 远程网络唤醒、关闭电脑
- 小爱同学唤醒、关闭电脑
- 电脑开机状态反馈

![image](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_1.png)
![image](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_2.png)
![image](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_3.png)

#### 视频演示
- [bilibili](https://www.bilibili.com/video/BV1bt4y1r7Uu/) 

# Arduino 编译环境搭建

详细步骤参见  [arduino 中文社区](https://www.arduino.cn/thread-83174-1-1.html "arduino中文社区")
1. 下载安装 Arduino IDE
[Arduino 中文社区](https://www.arduino.cn/thread-5838-1-1.html "Arduino 中文社区")
[Arduino 官网](https://www.arduino.cc/en/software "Arduino 官网")

2. 安装 esp8266/esp32扩展包
[esp32 SDK](https://www.arduino.cn/thread-81194-1-1.html "esp32 SDK")
[esp8266 SDK](https://www.arduino.cn/thread-76029-1-1.html "esp8266 SDK")

3. 安装 blinker arduino支持库
[github](https://github.com/blinker-iot/blinker-library/archive/master.zip "github")
[blinker 官网](https://diandeng.tech/doc/sdk-download "blinker 官网")

4. 手机安装 blinker app
[github](https://github.com/blinker-iot/app-release/releases "github")
[blinker 官网](https://diandeng.tech/home "blinker 官网")

# 设备添加
每一个设备在blinker上都有一个唯一的密钥，blinker设备会使用该密钥认证设备身份，从而使用blinker云平台上的相关服务。
进入 blinker App，点击“添加设备”，进行设备添加。
选择独立设备，再选择WiFi接入，即可获取一个唯一的密钥。暂存这个密钥，此后程序中会使用到它。
**这里一定要使用阿里云服务商，否则小爱无法连接设备，语音提示“设备连接出问题了**

![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_1.jpg)
![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_2.jpg)
![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_3.jpg)
![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_4.jpg)

# 使用说明

1. **请确认你的电脑支持 网络唤醒**

2. 下载源码，使用 Arduino 打开 "udp_turn_off.ino" 文件，替换用户自定义变量
![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_6.jpg)

3. Arduino 中选择 esp32 开发板（一般来说选哪个都可以）
![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_5.jpg)

4. 编译上传到esp32开发板（编译成功后屏幕下方出现connect的时候要按一下boot键才能下载。）
![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_7.jpg)

5. 运行 C# 程序，选中开机启动（可选，为 **远程关机/小爱语音关机 **提供支持）

6. 进入 blinker App，添加按键，数据键名为 “btn-abc”（可选，为 **APP远程开/关机** 提供支持，显示文本不用管，会自动同步设备状态）
![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_8.jpg)

7. 修改开发板设备名为 “电脑”（可选，为 **小爱语音开/关机 **提供支持）
![](https://github.com/tty228/Python-100-Days/blob/master/res/udp_turn_off_9.jpg)

8. 打开米家-我的-其它平台设备-选择点灯科技，进去后绑定账号选择同步设备（可选，为 **小爱语音开/关机 **提供支持）

#### 代码鸣谢
- [帅比一号](https://post.smzdm.com/p/aoown0g7/) 
- [点灯科技](https://diandeng.tech/home) 
- [小渣渣](https://www.itsvse.com/thread-4806-1-4.html) 
- [谷歌搜索引擎](https://google.com/ncr) 
  等等等等等
