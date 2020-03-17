using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace test
{
[StructLayout(LayoutKind.Sequential)]

public unsafe class IC

{

    //对设备进行初始化

    [DllImport("Mwic_32.dll", EntryPoint = "auto_init", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

    public static extern int auto_init(int port, int baud);

    //设备密码格式

    [DllImport("Mwic_32.dll", EntryPoint = "setsc_md", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

    public static extern int setsc_md(int icdev, int mode);

    //获取设备当前状态

    [DllImport("Mwic_32.dll", EntryPoint = "get_status", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

    public static extern Int16 get_status(int icdev, Int16* state);

    //关闭设备通讯接口

    [DllImport("Mwic_32.dll", EntryPoint = "ic_exit", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

    public static extern int ic_exit(int icdev);

    //使设备发出蜂鸣声

    [DllImport("Mwic_32.dll", EntryPoint = "dv_beep", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

    public static extern int dv_beep(int icdev, int time);

    //向IC卡中写数据

    [DllImport("Mwic_32.dll", EntryPoint = "swr_4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

    public static extern int swr_4442(int icdev, int offset, int len, char* w_string);

    //核对卡密码 

    [DllImport("Mwic_32.dll", EntryPoint = "csc_4442", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]

    public static extern Int16 Csc_4442(int icdev, int len, [MarshalAs(UnmanagedType.LPArray)] byte[] p_string);



    [DllImport("Mwic_32.dll", EntryPoint = "srd_4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern int srd_4442(int icdev, int offset, int len, char* r_string);


}
}
