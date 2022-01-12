using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoftwareTeamXiangQi
{
    internal class HandClick
    { 
        public static int selectRow;
        public static int selectCol;
        public static bool finish = false;
        public static Button? FristButton,SecondButton;
        public static ValueTuple<Chess,Brush> record;


        public static void HandleClick(int row, int col, object sender)
        {
            switch (((Button)sender).Name)
            {
                case "悔棋":
                case "换棋":
                case "退出":
                    specialClick(row, col, sender);
                    break;
                default:
                    normalClick(row, col, sender);
                    break;
            }
        }

        public static void specialClick(int row, int col, object sender){
            switch (((Button)sender).Name)
            {
                case "换棋":
                    if (FristButton == null || FristButton.Background == Brushes.Transparent)
                        MessageBox.Show("You don't chose any chess");
                    else{
                        MainWindow.CleanHint();
                        MainWindow.board.model = play_model.origin_chess;                       
                    }
                    break;
                case "悔棋": 
                    if (SecondButton == null || finish != true)
                        MessageBox.Show("You don't move any chess");
                    else
                    {
                        int origin_row = MainWindow.buttons.IndexOf(SecondButton) / 9;
                        int origin_col = MainWindow.buttons.IndexOf(SecondButton) % 9;
                        int destation_row = MainWindow.buttons.IndexOf(FristButton) / 9;
                        int destation_col = MainWindow.buttons.IndexOf(FristButton) % 9;

                        MainWindow.board.chesses[origin_row, origin_col].MoveChess(origin_row, origin_col, destation_row, destation_col);
                        FristButton.Background = SecondButton.Background;
                        SecondButton.Background = record.Item2;
                        if (record.Item2 != Brushes.Transparent)
                            MainWindow.board.chesses[origin_row, origin_col] = record.Item1;

                        MainWindow.board.Turn(MainWindow.board.turn);
                        SecondButton = null;

                    }
                    break;
                case "退出":
                    System.Envionment.Exit(0);
                    break;
            }


        }
        public static void normalClick(int row, int col, object sender) {
            try
            {
                switch (MainWindow.board.model)
                {
                    case play_model.origin_chess:
                        finish = false;
                        MainWindow.board.selectChess(row, col);
                        selectRow = row;
                        selectCol = col;
                        MainWindow.Hint(selectRow, selectCol);
                        FristButton = (Button)sender;
                        break;
                    case play_model.destination:
                        if (MainWindow.board.chesses[row, col] != null && MainWindow.board.chesses[row, col].color == MainWindow.board.turn)
                            throw new Exception("You can't choose the same color chess");

                        if (MainWindow.board.chesses[selectRow, selectCol].CheckValidMove(row, col))
                        {
                            if (MainWindow.board.chesses[row, col] != null && MainWindow.board.chesses[row, col].Print == "将")
                            {
                                if (MainWindow.board.chesses[row, col].color == Color.red)
                                    MainWindow.fail = Fail.red;
                                                                
                                else
                                    MainWindow.fail = Fail.black;
                            }
                            SecondButton = (Button)sender;
                            if(SecondButton.Background != Brushes.Transparent)
                            {
                                record.Item1 = MainWindow.board.chesses[row, col];
                                record.Item2 = SecondButton.Background;
                            }
                            else
                                record.Item2 = Brushes.Transparent;
                            MainWindow.board.chesses[selectRow, selectCol].MoveChess(selectRow, selectCol, row, col);
                            SecondButton.Background = FristButton.Background;
                            FristButton.Background = Brushes.Transparent;
                            MainWindow.CleanHint();
                            MainWindow.board.model = play_model.origin_chess;
                            MainWindow.board.Turn(MainWindow.board.turn);
                            finish = true;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            if (MainWindow.fail != Fail.no)
                MessageBox.Show(MainWindow.board.chesses[row, col].color.ToString() + " Win!");
        }
    }
}
