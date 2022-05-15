using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace udp_turn_off
{
    internal class ChinaDate
    {
        #region 参数和变量
        private static ChineseLunisolarCalendar china = new ChineseLunisolarCalendar();
        private static Hashtable gHoliday = new Hashtable();
        private static Hashtable nHoliday = new Hashtable();
        private static Hashtable wHoliday = new Hashtable();
        private static Hashtable Blessings = new Hashtable();
        private static readonly string[] JQ = { "节气：小寒", "节气：大寒", "节气：立春", "节气：雨水", "节气：惊蛰", "节气：春分", "节气：清明", "节气：谷雨", "节气：立夏", "节气：小满", "节气：芒种", "节气：夏至", "节气：小暑", "节气：大暑", "节气：立秋", "节气：处暑", "节气：白露", "节气：秋分", "节气：寒露", "节气：霜降", "节气：立冬", "节气：小雪", "节气：大雪", "节气：冬至" };
        private static readonly int[] JQData = { 0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758 };
        //readonly 使字段只读
        #endregion

        #region 节日信息及祝福语
        static ChinaDate()
        {
            //公历节日
            gHoliday.Add("0101", "元旦");
            gHoliday.Add("0214", "情人节");
            gHoliday.Add("0305", "学雷锋纪念日");
            gHoliday.Add("0308", "三八妇女节");
            gHoliday.Add("0312", "植树节");
            gHoliday.Add("0314", "白色情人节");
            gHoliday.Add("0315", "消费者权益保护日");
            gHoliday.Add("0401", "愚人节");
            gHoliday.Add("0501", "劳动节");
            gHoliday.Add("0502", "劳动节假日");
            gHoliday.Add("0503", "劳动节假日");
            gHoliday.Add("0504", "五四青年节");
            gHoliday.Add("0531", "世界无烟日");
            gHoliday.Add("0601", "国际儿童节");
            gHoliday.Add("0605", "世界环境保护日");
            gHoliday.Add("0701", "建党节 与 香港回归纪念日");
            gHoliday.Add("0801", "建军节");
            gHoliday.Add("0808", "中国男子日 也叫 中国爸爸节");
            gHoliday.Add("0815", "抗日战争胜利纪念日");
            gHoliday.Add("0909", "毛泽东逝世纪念日");
            gHoliday.Add("0910", "教师节");
            gHoliday.Add("0918", "九·一八事变纪念日");
            gHoliday.Add("1001", "国庆节");
            gHoliday.Add("1002", "国庆节假日");
            gHoliday.Add("1003", "国庆节假日");
            gHoliday.Add("1111", "光棍节");
            gHoliday.Add("1201", "世界艾滋病日");
            gHoliday.Add("1220", "澳门回归纪念日");
            gHoliday.Add("1224", "平安夜");
            gHoliday.Add("1225", "圣诞节");
            gHoliday.Add("1226", "毛泽东诞辰纪念日");

            //农历节日
            nHoliday.Add("0101", "春节");
            nHoliday.Add("0115", "元宵节");
            nHoliday.Add("0303", "三月三");
            nHoliday.Add("0505", "端午节");
            nHoliday.Add("0707", "七夕情人节");
            nHoliday.Add("0815", "中秋节");
            nHoliday.Add("0909", "重阳节");
            nHoliday.Add("1208", "腊八节");

            //公历5月第2个周日是母亲节
            //new WeekHolidayStruct(5, 2, 1, "母亲节"), 
            //公历6月第3个周日是父亲节
            //new WeekHolidayStruct(6, 3, 1, "父亲节"),
            //公历11月第4个周四是感恩节
            //new WeekHolidayStruct(11, 4, 5, "感恩节")

            wHoliday.Add("050207", "母亲节");
            wHoliday.Add("060307", "父亲节");
            wHoliday.Add("110404", "感恩节");

            //节日祝福语
            Blessings.Add("元旦", "新年新气象，来年更辉煌，让我们再一次踏上征程！");
            Blessings.Add("情人节", "特别的礼物，送给特别的Ta，这个特别的日子，祝你有一个特别的回忆");
            Blessings.Add("学雷锋纪念日", "送人玫瑰，手有余香，助人为乐，快乐一生。");
            Blessings.Add("三八妇女节", "向辛勤耕耘在各条战线的妇女工作者表示崇高的敬意。");
            Blessings.Add("植树节", "多一份关爱，多一份呵护，从我做起，爱护景区一草一木。");
            Blessings.Add("白色情人节", "白色情人节是女孩回赠男孩礼物日子，情人节别饶了那小子");
            Blessings.Add("消费者权益日", "消费者的 “四项权利”：有权获得安全保障；有权获得正确资料；有权自由决定选择；有权提出消费意见。你知道了吗？");
            Blessings.Add("愚人节", "转帐通知：我们已经将 20 万元按要求转入你的账户，请在 2 秒钟内确认，过期作废。");
            Blessings.Add("劳动节", "人生至少有两次冲动，一次为了奋不顾身的爱情，一次为了说走就走的旅行。");
            Blessings.Add("劳动节假日", "人生至少有两次冲动，一次为了奋不顾身的爱情，一次为了说走就走的旅行。");
            Blessings.Add("五四青年节", "少年强,则国强;少年富,则国富;少年屹立于世界,则国屹立于世界");
            Blessings.Add("世界无烟日", "千山鸟飞绝，万径人踪灭。吞云吐雾中，物物皆湮灭。愿您摒弃手中香烟，共度幸福人生。");
            Blessings.Add("国际儿童节", "六一儿童节到了，谁还不是个宝宝呢？我凭农药技术过的节，凭啥说我老。");
            Blessings.Add("世界环境保护日", "保护蓝天碧水，造福子孙后代，保护环境，就是保护我们赖以生存的家园。");
            Blessings.Add("建党节 与 香港回归纪念日", "权为民所用，情为民所系，利为民所谋，全心全意为人民服务！");
            Blessings.Add("建军节", "岁月静好,因为有他们负重前行，风雨兼程，战旗飞扬，致敬，为了我们可爱的人民子弟兵。");
            Blessings.Add("中国男子日 也叫 中国爸爸节", "父亲如山，一路相伴；父爱无言，细水长流。别忘了给老爸一个温馨的父亲节。");
            Blessings.Add("抗日战争胜利纪念日", "中国人民抗日战争胜利万岁！深切怀念在世界反法西斯战争中牺牲和殉难的人们！");
            Blessings.Add("毛泽东逝世纪念日", "毛泽东思想永放光芒，他永远活在我们心中。");
            Blessings.Add("教师节", "学高为师，身正为范。教人为善，师古圣贤。");
            Blessings.Add("九·一八事变纪念日", "九·一八！勿忘国耻！");
            Blessings.Add("国庆节", "人生最好的旅行，就是你在一个陌生的地方，发现一种久违的感动。");
            Blessings.Add("国庆节假日", "人生最好的旅行，就是你在一个陌生的地方，发现一种久违的感动。");
            Blessings.Add("光棍节", "今天，是想过光棍节，还是想过剁手节？");
            Blessings.Add("世界艾滋病日", "预防艾滋病，你我同参与，正确使用安全套，避免共用注射器，有效预防艾滋病。");
            Blessings.Add("澳门回归纪念日", "澳门回归纪念日，骨肉兄弟不分离，祝福祖国，祝福澳门，走向更加辉煌的明天！");
            Blessings.Add("平安夜", "平安夜，送你一颗平安果，祝你一生平平安安，和你所爱的人，共享幸福之果。");
            Blessings.Add("圣诞节", "许一个美好的心愿祝你圣诞快乐连连，送一份美妙的感觉祝你圣诞万事圆圆，送一份漂亮的礼物祝你微笑甜甜。");
            Blessings.Add("毛泽东诞辰纪念日", "学习和发扬毛泽东同志等老一辈无产阶级革命家的崇高精神和品德，凝聚和振奋民族精神，共建社会主义和谐社会。");
            Blessings.Add("除夕", "新年的钟声里，我举起杯，任一弯晶莹的思绪，在杯底悄悄沉淀，深深地祝福你快乐！");
            Blessings.Add("春节", "新的一年，新的开始，新的起点，新的征程，祝你万事如意，事业高升！");
            Blessings.Add("元宵节", "元宵佳节吃元宵，合家欢聚其乐融融。张灯结彩龙狮闹，团圆欢乐一家好。");
            Blessings.Add("三月三", "三月三又称 “上巳节”。在广西为法定假日，是中国多个民族的传统节日，其中以壮族为典型，壮族传统踏青歌节，壮族青年男女聚集街头欢歌、汇聚江边饮宴。也是壮族祭祖、祭拜盘古、布洛陀始祖的重要日子。");
            Blessings.Add("端午节", "一片清香的粽叶，很薄；一颗美味的粽子，很甜；一壶醇香的美酒，很香；一声诚挚的祝福，送您：恭祝您端午节快乐！");
            Blessings.Add("七夕情人节", "一束鲜花，捧在手中，甜在心里。一盒巧克力，甜在嘴里，暖在心田。别忘了给心爱的Ta一份甜蜜。");
            Blessings.Add("中秋节", "佳节共赏天上月，中秋一品人间情。遥寄相思中秋梦，千里故人何处逢。祝您中秋快乐。");
            Blessings.Add("重阳节", "重阳之日，登高祈福、佩插茱萸，享宴高会，感恩敬老。敬老从心开始，助老从我做起。");
            Blessings.Add("腊八节", "相传十二月初八这天是佛祖释迦牟尼成道之日，各寺院都用香谷和果实做成粥来赠送给门徒和善男信女们。传说喝了这种粥以后，就可以得到佛祖的保佑，因此，腊八粥也叫 “福寿粥”“福德粥” 和 “佛粥”。");
            Blessings.Add("母亲节", "谁言寸草心，报得三春晖？献给最伟大的母亲，哺育之恩永铭在心。");
            Blessings.Add("父亲节", "父爱如河，细长而源源；父亲如山，无声却稳重。他是血与脉的相通相融，让我们一起来道声：“爸爸，您辛苦了！”");
            Blessings.Add("感恩节", "感恩不需要惊天动地，只需要你的一句问候，一声呼唤，一丝感慨，一个微笑。");
            Blessings.Add("节气：小寒", "小寒，标志着季冬时节的正式开始，根据中国气象资料，小寒是气温最低的节气，只有少数年份的大寒气温低于小寒。别忘了多穿衣服。");
            Blessings.Add("节气：大寒", "大寒，是二十四节气中的最后一个节气，此时寒潮南下频繁，是中国部分地区一年中的最冷时期，雨雪天气频繁，雨雪天气频繁，要注意保暖哦。");
            Blessings.Add("节气：立春", "立春，立春，反映着一年四季的更替，需拜祭春神、太岁、土地神等，敬天法祖，华南地区迎来春天的气息。");
            Blessings.Add("节气：雨水", "雨水，雨水节气前后，万物开始萌动，春天就要到了。气温回升、冰雪融化、降水增多，故取名为雨水");
            Blessings.Add("节气：惊蛰", "惊蛰，标志着仲春时节的开始，蛰虫惊醒，天气转暖，渐有春雷，中国大部分地区进入春耕季节。万物出乎震，震为雷，故曰惊蛰。");
            Blessings.Add("节气：春分", "春分，也是节日和祭祀庆典，古代帝王有春天祭日，秋天祭月的礼制。民间活动上，一般算做踏青的正式开始。");
            Blessings.Add("节气：清明", "清明，是传统的重大春祭节日，扫墓祭祀、缅怀祖先，是中华民族数千年以来的优良传统。清明时节雨纷纷，一束鲜花祭故人。");
            Blessings.Add("节气：谷雨", "谷雨，清明断雪，谷雨断霜，谷雨是春季最后一个节气，谷雨节气的到来意味着寒潮天气基本结束，每年第一场大雨一般出现在这段时间。");
            Blessings.Add("节气：立夏", "立夏，夏季的第一个节气，表示盛夏时节的正式开始。实际上，我国只有南部地区是真正的夏季，东北等地这时则刚刚进入春季。");
            Blessings.Add("节气：小满", "小满，其含义是夏熟作物的籽粒开始灌浆饱满，但还未成熟，只是小满，还未大满。在小满的最后一个时段，麦子开始成熟。");
            Blessings.Add("节气：芒种", "芒种，芒种的 “芒” 字，是指麦类等有芒植物的收获，芒种的 “种” 字，是指谷黍类作物播种的节令。“芒种” 二字谐音，表明一切作物都在 “忙着种” 了。");
            Blessings.Add("节气：夏至", "夏至，太阳直射地面的位置到达一年的最北端，几乎直射北回归线，此时，北半球各地的白昼时间达到全年最长。对于北回归线及其以北的地区来说，夏至日也是一年中正午太阳高度最高的一天。");
            Blessings.Add("节气：小暑", "小暑，暑，表示炎热的意思，小暑为小热，还不十分热。意指天气开始炎热，但还没到最热。");
            Blessings.Add("节气：大暑", "大暑，节气正值 “三伏天” 里的 “中伏” 前后，是一年中最热的时期，气温最高，同时，很多地区的旱、涝、风灾等各种气象灾害也最为频繁。");
            Blessings.Add("节气：立秋", "立秋，是秋天的第一个节气，标志着孟秋时节的正式开始：“秋” 就是指暑去凉来。到了立秋，梧桐树开始落叶，因此有 “落叶知秋” 的成语。");
            Blessings.Add("节气：处暑", "处暑，即为 “出暑”，是炎热离开的意思。处暑以后，除华南和西南地区外，我国大部分地区雨季即将结束，降水逐渐减少。");
            Blessings.Add("节气：白露", "白露，天气渐转凉，会在清晨时分发现地面和叶子上有许多露珠，这是因夜晚水汽凝结在上面，故名白露。进入 “白露”，晚上会感到一丝丝的凉意。");
            Blessings.Add("节气：秋分", "秋分，秋分之 “分” 为 “半” 之意。昼夜时间均等，气候由热转凉。北半球各地开始昼短夜长，即一天之内白昼开始短于黑夜；南半球相反。故秋分也称降分。");
            Blessings.Add("节气：寒露", "寒露，表示秋季时节的正式结束，气温逐渐下降。寒露时节，南岭及以北的广大地区均已进入秋季，东北进入深秋，西北地区已进入或即将进入冬季。");
            Blessings.Add("节气：霜降", "霜降，霜降节气含有天气渐冷、初霜出现的意思，意味着冬天即将开始。霜降时节，养生保健尤为重要，民间有谚语 “一年补透透，不如补霜降”。");
            Blessings.Add("节气：立冬", "立冬，立，建始也，表示冬季自此开始。立冬前后，中国大部分地区降水显著减少。中国北方地区大地封冻，农林作物进入越冬期。水始冰。水面初凝，未至于坚也。地始冻。土气凝寒，未至于拆。");
            Blessings.Add("节气：小雪", "小雪，进入该节气，中国广大地区西北风开始成为常客，气温下降，逐渐降到 0℃以下，但大地尚未过于寒冷，虽开始降雪，但雪量不大，故称小雪。");
            Blessings.Add("节气：大雪", "大雪，标志着仲冬时节的正式开始。大雪的意思是天气更冷，降雪的可能性比小雪时更大了，并不指降雪量一定很大。");
            Blessings.Add("节气：冬至", "冬至，被视为冬季的大节日，又被称为 “小年”，是冬季祭祖大节，古人视为吉日，认为自冬至起，天地阳气开始兴作渐强，曰：冬至一阳生。");
        }
        #endregion

        #region 农历获取
        /// <summary>
        /// 获取农历
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetChinaDate(DateTime dt)
        {
            if (dt > china.MaxSupportedDateTime || dt < china.MinSupportedDateTime)
            {
                //日期范围：1901 年 2 月 19 日 - 2101 年 1 月 28 日
                throw new Exception(string.Format("日期超出范围！必须在{0}到{1}之间！", china.MinSupportedDateTime.ToString("yyyy-MM-dd"), china.MaxSupportedDateTime.ToString("yyyy-MM-dd")));
            }
            string str = string.Format("{0} {1}{2}", GetYear(dt), GetMonth(dt), GetDay(dt));
            string strJQ = GetSolarTerm(dt);
            if (strJQ != "")
            {
                str += " (" + strJQ + ")";
            }
            string strHoliday = GetHoliday(dt);
            if (strHoliday != "")
            {
                str += " " + strHoliday;
            }
            string strChinaHoliday = GetChinaHoliday(dt);
            if (strChinaHoliday != "")
            {
                str += " " + strChinaHoliday;
            }

            return str;
        }

        /// <summary>
        /// 获取农历年份
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetYear(DateTime dt)
        {
            int yearIndex = china.GetSexagenaryYear(dt);
            string yearTG = " 甲乙丙丁戊己庚辛壬癸";
            string yearDZ = " 子丑寅卯辰巳午未申酉戌亥";
            string yearSX = " 鼠牛虎兔龙蛇马羊猴鸡狗猪";
            int year = china.GetYear(dt);
            int yTG = china.GetCelestialStem(yearIndex);
            int yDZ = china.GetTerrestrialBranch(yearIndex);

            string str = string.Format("[{1}]{2}{3}{0}", year, yearSX[yDZ], yearTG[yTG], yearDZ[yDZ]);
            return str;
        }

        /// <summary>
        /// 获取农历月份
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetMonth(DateTime dt)
        {
            int year = china.GetYear(dt);
            int iMonth = china.GetMonth(dt);
            int leapMonth = china.GetLeapMonth(year);
            bool isLeapMonth = iMonth == leapMonth;
            if (leapMonth != 0 && iMonth >= leapMonth)
            {
                iMonth--;
            }

            string szText = "正二三四五六七八九十";
            string strMonth = isLeapMonth ? "闰" : "";
            if (iMonth <= 10)
            {
                strMonth += szText.Substring(iMonth - 1, 1);
            }
            else if (iMonth == 11)
            {
                strMonth += "十一";
            }
            else
            {
                strMonth += "腊";
            }
            return strMonth + "月";
        }

        /// <summary>
        /// 获取农历日期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDay(DateTime dt)
        {
            int iDay = china.GetDayOfMonth(dt);
            string szText1 = "初十廿三";
            string szText2 = "一二三四五六七八九十";
            string strDay;
            if (iDay == 20)
            {
                strDay = "二十";
            }
            else if (iDay == 30)
            {
                strDay = "三十";
            }
            else
            {
                strDay = szText1.Substring((iDay - 1) / 10, 1);
                strDay = strDay + szText2.Substring((iDay - 1) % 10, 1);
            }
            return strDay;
        }
        #endregion

        #region 节日获取
        /// <summary>
        /// 获取节气
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetSolarTerm(DateTime dt)
        {
            DateTime dtBase = new DateTime(1900, 1, 6, 2, 5, 0);
            DateTime dtNew;
            double num;
            int y;
            string strReturn = "";

            y = dt.Year;
            for (int i = 1; i <= 24; i++)
            {
                num = 525948.76 * (y - 1900) + JQData[i - 1];
                dtNew = dtBase.AddMinutes(num);
                if (dtNew.DayOfYear == dt.DayOfYear)
                {
                    strReturn = JQ[i - 1];
                }
            }

            return strReturn;
        }

        /// <summary>
        /// 获取公历节日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetHoliday(DateTime dt)
        {
            string strReturn = "";
            object g = gHoliday[dt.Month.ToString("00") + dt.Day.ToString("00")];
            if (g != null)
            {
                strReturn = g.ToString();
            }

            return strReturn;
        }

        /// <summary>
        /// 获取农历节日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetChinaHoliday(DateTime dt)
        {
            string strReturn = "";
            int year = china.GetYear(dt);
            int iMonth = china.GetMonth(dt);
            int leapMonth = china.GetLeapMonth(year);
            int iDay = china.GetDayOfMonth(dt);
            if (china.GetDayOfYear(dt) == china.GetDaysInYear(year))
            {
                strReturn = "除夕";
            }
            else if (leapMonth != iMonth)
            {
                if (leapMonth != 0 && iMonth >= leapMonth)
                {
                    iMonth--;
                }
                object n = nHoliday[iMonth.ToString("00") + iDay.ToString("00")];
                if (n != null)
                {
                    if (strReturn == "")
                    {
                        strReturn = n.ToString();
                    }
                    else
                    {
                        strReturn += " " + n.ToString();
                    }
                }
            }

            return strReturn;
        }

        /// <summary>
        /// 获取第几个星期的节日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string WeekDayHoliday(DateTime dt)
        {
            string strReturn = "";
            //DateTime firstDay = new DateTime(dt.Year, dt.Month, 1);//生成当月第一天
            //int i = (int)firstDay.DayOfWeek;//计算当月第一天是星期几
            //if (i == 0) { i = 7; }
            //int firWeekDays = 8 - i; //计算第一周剩余天数
            int i_dt = (int)dt.DayOfWeek;//计算dt是星期几
            if (i_dt == 0) { i_dt = 7; }
            object g = wHoliday[dt.Month.ToString("00") + Math.Ceiling((double)dt.Day / 7).ToString("00") + i_dt.ToString("00")];
            if (g != null)
            {
                strReturn = g.ToString();
            }

            return strReturn;
        }


        /// <summary>
        /// 获取下一个节日的日期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string NextDay(DateTime dt)
        {
            string strReturn = "";
            for (int i = 1; i <= 365; i++)
            {
                DateTime Next_dt = dt.AddDays(i);
                if (Holiday(Next_dt) != "")
                {
                    strReturn = "距离下一个节日" + "\r\n" + Holiday(Next_dt) + " 还有 " + string.Format("{0}", i) + " 天" + "\r\n日期是：" + dt.AddDays(i).ToString("yyyy/MM/dd dddd");//应使用ToString， ToShortDateString会优先参照电脑用户设定

                    break;
                }
            }

            return strReturn;
        }

        /// <summary>
        /// 获取日期节日的总和
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string Holiday(DateTime dt)
        {
            string strReturn = "";
            string a = "";
            string b = "";
            string c = "";
            if (ChinaDate.GetSolarTerm(dt) != "")
            {
                if (ChinaDate.GetHoliday(dt) != "" || ChinaDate.GetChinaHoliday(dt) != "" || ChinaDate.WeekDayHoliday(dt) != "")
                { a = " 和 "; }
            }

            if (ChinaDate.GetHoliday(dt) != "")
            {
                if (ChinaDate.GetChinaHoliday(dt) != "" || ChinaDate.WeekDayHoliday(dt) != "")
                { b = " 和 "; }
            }

            if (ChinaDate.GetChinaHoliday(dt) != "")
            {
                if (ChinaDate.WeekDayHoliday(dt) != "")
                { c = " 和 "; }
            }
            strReturn = ChinaDate.GetSolarTerm(dt) + a + ChinaDate.GetHoliday(dt) + b + ChinaDate.GetChinaHoliday(dt) + c + ChinaDate.WeekDayHoliday(dt);

            return strReturn;
        }
        #endregion

        #region 祝福语获取
        /// <summary>
        /// 字符获取节日祝福语
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string Str_Blessings(string str)
        {
            string strReturn = "";
            object g = Blessings[str];
            if (g != null)
            {
                strReturn = g.ToString();
            }

            return strReturn;
        }

        /// <summary>
        /// 获取节日祝福语
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string HolidayBlessings(DateTime dt)
        {
            string strReturn = "";
            if (ChinaDate.Holiday(dt) != "")
            {
                string[] a = new string[] { };
                if (ChinaDate.GetSolarTerm(dt) != "")
                {
                    List<string> b = a.ToList();//C#不允许改变str数组长度，先转 List，处理完了再丢给数组。
                    b.Add(ChinaDate.Str_Blessings(ChinaDate.GetSolarTerm(dt)));//Array.CopyTo 方法来将其 Copy 到一个新数组中。
                    a = b.ToArray();//处理完成，重新给数组赋值。
                }

                if (ChinaDate.GetHoliday(dt) != "")
                {
                    List<string> b = a.ToList();//C#不允许改变str数组长度，先转 List，处理完了再丢给数组。
                    b.Add(ChinaDate.Str_Blessings(ChinaDate.GetHoliday(dt)));//Array.CopyTo 方法来将其 Copy 到一个新数组中。
                    a = b.ToArray();//处理完成，重新给数组赋值。
                }

                if (ChinaDate.GetChinaHoliday(dt) != "")
                {
                    List<string> b = a.ToList();//C#不允许改变str数组长度，先转 List，处理完了再丢给数组。
                    b.Add(ChinaDate.Str_Blessings(ChinaDate.GetChinaHoliday(dt)));//Array.CopyTo 方法来将其 Copy 到一个新数组中。
                    a = b.ToArray();//处理完成，重新给数组赋值。
                }
                if (ChinaDate.WeekDayHoliday(dt) != "")
                {
                    List<string> b = a.ToList();//C#不允许改变str数组长度，先转 List，处理完了再丢给数组。
                    b.Add(ChinaDate.Str_Blessings(ChinaDate.WeekDayHoliday(dt)));//Array.CopyTo 方法来将其 Copy 到一个新数组中。
                    a = b.ToArray();//处理完成，重新给数组赋值。
                }
                Random r = new Random();
                strReturn = a[r.Next(a.Length)];//确定数组的下标，从而随机一个数组。

            }

            return strReturn;
        }
        #endregion

        #region 欢迎语
        /// <summary>
        /// 生成气泡信息
        /// </summary>
        public static string logo()
        {
            //MessageBox.Show (DateTime.Now.ToString("HH"));
            //string h = DateTime.Now.ToString("HH");
            string logo = "";
            if (Holiday(DateTime.Now) == "" && Holiday(DateTime.Now.AddDays(1)) == "")
            {
                int h = DateTime.Now.Hour;
                if (h >= 6 && h <= 9)
                { logo = "早上好！今天又是元气满满的一天！"; }
                else if (h >= 10 && h <= 11)
                { logo = "上午好！祝你工作顺利！"; }
                else if (h == 12)
                { logo = "午休时间，休息一会吧。"; }
                else if (h >= 13 && h <= 15)
                { logo = "下午好！泡一杯清茶，迎一股淡香。"; }
                else if (h >= 16 && h <= 17)
                { logo = "忙碌了一天，辛苦了！"; }
                else if (h >= 18 && h <= 20)
                { logo = "晚上好！忙碌的时候不要忘了先填饱肚子哦！"; }
                else if (h >= 21 && h <= 22)
                { logo = "夜深了，注意身体……"; }
                else
                {
                    Random rd = new Random();
                    int i = rd.Next(1, 9);
                    //MessageBox.Show(string.Format("{0}", i));
                    if (i == 1)
                    { logo = "我欲修仙,法力无边 (╰_╯)#"; }
                    else if (i == 2)
                    { logo = "听说半夜不睡觉的人,都是有故事的人"; }
                    else if (i == 3)
                    { logo = "长夜漫漫，无心睡眠，原来晶晶姑娘你也睡不着啊 *^‧^*"; }
                    else if (i == 4)
                    { logo = "摸鱼一时爽，一直摸就一直爽 '\'(≧▽≦)/"; }
                    else if (i == 5)
                    { logo = "假如今天生活欺骗了你，不要悲伤，不要哭泣，因为明天生活还会继续欺骗你。"; }
                    else if (i == 6)
                    { logo = "您本次摸鱼共享时 55 分钟，已击败了全公司 60% 的员工，再接再厉！"; }
                    else if (i == 7)
                    { logo = "只要你好好努力、发奋工作，那么到了明年这时候，老板就能换辆更好的车啦！"; }
                    else if (i == 8)
                    { logo = "今晚，又是一个不眠夜……"; }
                }
            }

            if (Holiday(DateTime.Now) != "")
            {
                logo = "今天是：" + Holiday(DateTime.Now);
                if (HolidayBlessings(DateTime.Now) != "")
                { logo = logo + "\r\n" + HolidayBlessings(DateTime.Now); }
                if (Holiday(DateTime.Now.AddDays(1)) != "")
                {
                    logo = logo + "\r\n" + "明天是：" + Holiday(DateTime.Now.AddDays(1));
                    //if (HolidayBlessings(DateTime.Now.AddDays(1)) != "")
                    //{ logo = logo + "\r\n" + HolidayBlessings(DateTime.Now.AddDays(1)); }
                }
            }
            else
            {
                if (Holiday(DateTime.Now.AddDays(1)) != "")
                {
                    logo = "明天是：" + Holiday(DateTime.Now.AddDays(1));
                    if (HolidayBlessings(DateTime.Now.AddDays(1)) != "")
                    { logo = logo + "\r\n" + HolidayBlessings(DateTime.Now.AddDays(1)); }
                }
            }
            return logo;
        }
        #endregion
    }

}
