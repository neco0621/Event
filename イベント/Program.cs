using System.Transactions;

namespace イベント
{
    internal class Program
    {
        /*イベント*/
        //イベントとはプログラム実行中のオブジェクトの状態を通知する仕組みです。
        //例 : ボタンが押された。ダウンロードが完了した。など
        //このイベントに応じて実行される処理をイベントハンドラーといいます。
        //このイベントは内部的にはデリゲートで実装されていて、
        //イベントに応じてデリゲートを実行するという形で処理されます。
        static void Main(string[] args)
        {
            /*イベントハンドラーの作成*/
            Console.WriteLine("/*イベントハンドラーの作成*/");
            EventSample x = new EventSample();//インスタンスの作成
            x.ValueChanged += new EventHandler<ValueChangeEventArgs>(x_ValueChanged);
            x.Value = 18;

            /*色々なイベントハンドラーの作成*/
            Console.WriteLine("/*イベントハンドラーの作成*/");
            EventSample y = new EventSample();//インスタンスの作成
            y.ValueChanged += new EventHandler<ValueChangeEventArgs>(x_ValueChanged);
            y.ValueChanged += y_ValueChanged;
            y.ValueChanged += delegate (object s1, ValueChangeEventArgs e1) { Console.WriteLine(e1.NewValue); };
            y.ValueChanged += (s1, e2) => Console.WriteLine(e2.NewValue);//ラムダ式
            y.Value = 19;//イベントの実行


            /*シンプルなイベントハンドラー*/
            Console.WriteLine("/*シンプルなイベントハンドラー*/");
            Simple simple = new Simple();
            simple.ValueChanged += (s,e) => Console.WriteLine("!?");
            Console.ReadLine();//エンター待ち
            simple.Value = 2;
        }

        /*シンプルなイベントハンドラー*/
        class Simple
        {
            public event EventHandler ValueChanged;
            private int _value = 0;
            public int Value
            {
                get { return _value; }
                set
                {
                    _value = value;
                    if (ValueChanged == null) return;
                    ValueChanged(this, EventArgs.Empty);
                }
            }
        }
        /*イベントを持つクラスの作成*/
        //プロパティの値が変化した際にそれを通知するイベントを持つクラスを作成する。
        //プロパティValueが変化した際に、ValueChangeイベントが発生するようにする。

        /*イベント引数*/
        public class ValueChangeEventArgs : EventArgs
        {
            public int NewValue { get; set; }
        }
        //クラスの作成
        public class EventSample
        {
            /*イベントハンドラー型の変数の作成*/
            public event EventHandler<ValueChangeEventArgs> ValueChanged;

            /*イベント通知のメソッドの作成*/
            protected virtual void OnValueChanged(int newValue)
            {
                if (ValueChanged == null) { return; } //エラー処理

                //新しい値を渡すための引数の作成
                ValueChangeEventArgs args = new ValueChangeEventArgs();
                args.NewValue = newValue;//値を代入
                ValueChanged(this, args);//値変化の通知
            }

            /*イベントを用いたVlaueの作成*/
            private int _value;
            public int Value
            {
                get { return _value; }
                set 
                {
                    if (_value == value) return;
                    _value = value;
                    OnValueChanged(value);
                }
            }

        }   

        /*イベントハンドラーの作成*/
        static void x_ValueChanged(object sender, ValueChangeEventArgs e)
        {
            Console.WriteLine(e.NewValue);
        }

        static void y_ValueChanged(object sender, ValueChangeEventArgs e)
        {
            Console.WriteLine(e.NewValue);
        }
    }
}