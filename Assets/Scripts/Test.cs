using System;
using System.Collections;
using System.IO;
using Gif2Textures;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using File = System.IO.File;

public class Test : RawImage
{
    public enum PLAYSTATE
    {
        ONCE = 0,
        LOOP
    }
    [Tooltip("播放方式")] public PLAYSTATE m_state = PLAYSTATE.LOOP;
    [NonSerialized]public int FrameIndex = 0;
    private bool m_bIsPlayer;
    private float m_frameSecond = 0.045f;
    [NonSerialized]public int FrameCount;
    GifFrames m_GifFrames = null;
    private Coroutine m_Coroutine;
    protected override void Start()
    {
        base.Start();
        PlayGif();
    }

    public void PlayGif()
    {
        var texture1 = (Texture2D)texture;
        if (texture1)
        {
            MemoryStream ms = new MemoryStream(texture1.GetRawTextureData());
            m_GifFrames = new GifFrames();
            if (m_GifFrames.Load(ms, false))
            {
                FrameCount = m_GifFrames.GetFrameCount();
                m_frameSecond = 1.0f / FrameCount;
                Play();
            }
        }
    }

    private void Play()
    {
        m_bIsPlayer = true;
        m_Coroutine = StartCoroutine(PlayUpdate());
    }

    IEnumerator PlayUpdate()
    {
        float oldTime = Time.time;
        //死循环运算
        while (true)
        {
            #region 替换为下一张
            ++FrameIndex;//索引+ addCount  相当于+1  如果是pingpong 则可能addCount == -1
            //如果索引超出范围
            if (FrameIndex >= FrameCount || FrameIndex < 0)
            {
                //不同状态有不同的处理方式
                if (m_state == PLAYSTATE.ONCE)
                {
                    Stop();
                    break;
                }
                else if (m_state == PLAYSTATE.LOOP)
                {
                    FrameIndex = 0;//如果是循环则从0开始
                }
            }
            ChangeImage();//替换图片
            #endregion
            yield return new WaitForSeconds(m_frameSecond);//暂停协程,开始其他运算
        }
        yield return null;
    }

    private void ChangeImage()
    {
        Texture2D texTemp;
        float delay;
        m_GifFrames.GetNextFrame(out texTemp, out delay);
        texture = texTemp;

    }

    private void Stop()
    {
        m_bIsPlayer = false;//记录是否在播放
        FrameIndex = 0;//返回为第一帧
        m_GifFrames.Restart();
        StopAllCoroutines();//停止协程
    }
}
