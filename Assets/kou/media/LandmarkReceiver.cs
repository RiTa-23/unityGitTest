using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class LandmarkReceiver : MonoBehaviour
{
    public string host = "192.168.1.16"; // PythonサーバーのIPアドレス
    public int port = 65432; // Pythonサーバーのポート番号

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            // Pythonサーバーに接続
            TcpClient client = new TcpClient(host, port);

            // データを受信するストリームを取得
            NetworkStream stream = client.GetStream();

            // データ受信用のバッファー
            byte[] buffer = new byte[4096];

            // データ受信ループ
            while (true)
            {
                // データを受信
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                
                // 受信したバイト数が0の場合は接続が切れたことを示すため、ループを終了する
                if (bytesRead == 0)
                {
                    Debug.LogError("Connection closed by the server11.");
                    break;
                }

                // 受信したデータを文字列に変換
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // 受信したJSONデータをコンソールに表示
                Debug.Log("Received JSON data from Python: " + receivedData);
            }

            // 接続を閉じる
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to connect to server: " + e);
        }
    }
}
