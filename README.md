## Unity实现的FPS游戏 - 孙雨航
#### 以守望先锋为需求的实现
###### 2021/6/15 - 至今    

**概要**
------------
1. 使用Unity的、以守望先锋（Overwatch）为需求开发的简单的FPS游戏。目前实现了训练靶场中的部分内容，操作方面实现了基本的FPS功能。英雄复现方面目前有训练靶场机器人和麦克雷。UI部分，当前做了玩家界面的UI。  
2. 可执行文件在**Bulid**目录下    

**具体实现**
-------
**基础部分**    
1. 使用WASD进行人物移动，鼠标旋转视角，鼠标左键射击   
![移动与视角旋转](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/master/gifs/%E7%A7%BB%E5%8A%A8%E4%B8%8E%E8%A7%86%E8%A7%92%E6%97%8B%E8%BD%AC.gif "移动与视角旋转")
2. 左键射击与普通击中反馈。当子弹击中敌人躯干部位时，会有白色击中反馈闪动，敌方扣除子弹伤害数值的血量。
![左键射击与普通击中反馈](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/%E6%99%AE%E9%80%9A%E5%87%BB%E4%B8%AD%E5%8F%8D%E9%A6%88.gif "普通击中反馈")
3. 左键射击与爆头击中反馈。当子弹击中敌人头部时，会有红色击中反馈闪动，敌方扣除子弹伤害数值两倍的血量。
![左键射击与爆头击中反馈](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/爆头击中反馈.gif "爆头击中反馈")
4. 当弹匣中子弹不满时，按R键补充子弹。不同的武器有不同的上弹时间。
![补充子弹](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/补充子弹.gif "补充子弹")
5. 敌人血条缩放的效果。远离敌人后显示的敌人血条会缩小
![血条缩放](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/血条缩放.gif "血条缩放")
6. 敌方机器人目前有三种模式（与守望先锋中一致）
    - 静止不动的机器人    
    ![静止机器人](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/静止机器人.gif "静止机器人")
    - 静止射击的机器人    
    ![不移动射击机器人](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/不移动射击机器人.gif "不移动射击机器人")
    - 移动但不射击的机器人
    ![移动不射击机器人](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/移动不射击机器人.gif "移动不射击机器人")
7. 当玩家或敌人死亡后会在复活时间结束后在出生点复活。
![死亡与复活](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/死亡与复活.gif "死亡与复活")   
8. 大招自动充能和释放。当大招充能100%后按Q键释放大招。目前大招部分的实现只有使用后点数清零并上全子弹。
![大招使用与充能](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/大招使用与充能.gif "大招使用与充能")  

-------
**英雄部分**
1. 麦克雷    
    - E技能：闪光弹。使用后被闪光弹击中的敌人目标进入晕眩状态并无法进行操作，度过晕眩时间后恢复正常。技能使用后进入冷却。
    ![麦克雷闪光弹](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/麦克雷-闪光弹.gif "闪光弹效果")  
    - 左SHIFT技能：战术翻滚。配合方向键向目标方向进行翻滚，翻滚期间锁定视角，翻滚结束后自动补充弹匣中的子弹。当只按shift时朝视角方向进行翻滚。技能使用后进入冷却。
    ![麦克雷翻滚](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/麦克雷-翻滚.gif "战术翻滚效果")  
    - 右键射击：连发。一次性打光弹匣中的剩余子弹。
    ![麦克雷六连](https://raw.githubusercontent.com/sunyuhang01/OverwatchWithUnity/blob/master/gifs/麦克雷-六连.gif "六连效果")  
