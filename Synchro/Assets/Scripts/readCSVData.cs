using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class readCSVData : MonoBehaviour
{

    // 敵データ格納用の配列データ(とりあえず初期値はnull値)
    private int[,] stageMapDatas;

    [SerializeField]
    private string path;

#if NEW_VER
// CSVから切り分けられた文字列型２次元配列データ 
private string [,]sdataArrays; 
#endif

    //読み込めたか確認の表示用の変数
    private int height = 0;    //行数
    private int width = 0;    //列数

#if OLD_VER
// CSVデータ読み込み関数(旧バージョン)
// 引数：データパス
private int[,] readCSVData(string path)
{
    // 返り値の２次元配列
    int[,] readToIntData;

    // ストリームリーダーsrに読み込む
    // ※Application.dataPathはプロジェクトデータのAssetフォルダまでのアクセスパスのこと,
    StreamReader sr = new StreamReader(Application.dataPath + path);
    // ストリームリーダーをstringに変換
    string strStream = sr.ReadToEnd();

    // StringSplitOptionを設定(要はカンマとカンマに何もなかったら格納しないことにする)
    System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

    // 行に分ける
    string []lines = strStream.Split(new char[]{'\r','\n'},option);

    // カンマ分けの準備(区分けする文字を設定する)
    char []spliter = new char[1]{','};

    // 行数設定
    int heightLength = lines.Length;
    // 列数設定
    int widthLength = lines[0].Split(spliter, option).Length;

    // 返り値の2次元配列の要素数を設定
    readToIntData = new int[heightLength, widthLength];

    // カンマ分けをしてデータを完全分割
    for (int i = 0; i < heightLength; i++)
    {
        for (int j = 0; j < widthLength; j++)
        {
            //カンマ分け
            string [ ] readStrData = lines[i].Split(spliter, option);
            //型変換
            readToIntData[i, j] = int.Parse(readStrData[j]);
        }
    }

    // 確認表示用の変数(行数、列数)を格納する
    this.height = heightLength;    //行数   
    this.width  = widthLength;     //列数

    // 返り値
    return readToIntData;
}
#endif

#if NEW_VER

// CSVデータを文字列型２次元配列に変換する
//                      ファイルパス,変換される配列の値(参照渡し)
private void readCSVData(string path,ref string [ , ] sdata )
{
    // ストリームリーダーsrに読み込む
    StreamReader sr     = new StreamReader(path);
    // ストリームリーダーをstringに変換
    string strStream    = sr.ReadToEnd( );
    // StringSplitOptionを設定(要はカンマとカンマに何もなかったら格納しないことにする)
    System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

    // 行に分ける
    string [ ] lines = strStream.Split(new char [ ] { '\r', '\n' }, option);

    // カンマ分けの準備(区分けする文字を設定する)
    char [ ] spliter = new char [1] { ',' };

    // 行数設定
    int h = lines.Length;
    // 列数設定
    int widthLength = lines[0].Split(spliter, option).Length;

    // 返り値の2次元配列の要素数を設定
    sdata = new string [h, w];

    // 行データを切り分けて,2次元配列へ変換する
    for(int i = 0; i < h; i++)
    {
        string [ ] splitedData = lines [i].Split(spliter, option);

        for(int j = 0; j < w; j++)
        {
            sdata [i, j] = splitedData [j];
        }
    }

    // 確認表示用の変数(行数、列数)を格納する
    this.height = h;    //行数   
    this.width  = w;    //列数
}

// ２次元配列の型を文字列型から整数値型へ変換する
private void convert2DArrayType (ref string[,]sarrays,ref int [,]iarrays ,int h,int w)
{
    iarrays = new int[h,w];
    for(int i = 0; i < h; i++)
    {
        for(int j = 0; j < w; j++)
        {
            iarrays [i, j] = int.Parse(sarrays [i, j]);
        }
    }
}

#endif



    //確認表示用の関数
    //引数：2次元配列データ,行数,列数
    private void WriteMapDatas(int[,] arrays, int hgt, int wid)
    {
        for (int i = 0; i < hgt; i++)
        {

            for (int j = 0; j < wid; j++)
            {
                //行番号-列番号:データ値 と表示される
                Debug.Log(i + "-" + j + ":" + arrays[i, j]);
            }
        }
    }

    void Start()
    {
        //データパスを設定
        //このデータパスは、Assetフォルダ以下の位置を書くので/で階層を区切り、CSVデータ名まで書かないと読み込んでくれない

        //データを読み込む(引数：データパス)
#if OLD_VER
     this.stageMapDatas   = readCSVData(path); 
#endif
#if NEW_VER
     readCSVData(Application.dataPath + path, ref this.sdataArrays);
     convert2DArrayType(ref this.sdataArrays,ref this.stageMapDatas,this.height,this.width);
#endif

        WriteMapDatas(this.stageMapDatas, this.height, this.width);
    }

    void UpDate()
    {

    }
}
