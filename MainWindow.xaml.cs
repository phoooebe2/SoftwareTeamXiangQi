using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareTeamXiangQi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        // How to declare CUSTOM PROPERTIES
        public static readonly DependencyProperty XQRowProperty =
                                DependencyProperty.Register("XQRow",
                                                            typeof(int),
                                                            typeof(Button),
                                                            new PropertyMetadata(default(int)));
        public static readonly DependencyProperty XQColProperty =
                                DependencyProperty.Register("XQCol",
                                                            typeof(int),
                                                            typeof(Button),
                                                            new PropertyMetadata(default(int)));
        
        public static Board board = new Board();
        public static Fail fail = Fail.no;
        public static List<Button> buttons = new List<Button>();
        public void CreateGrid()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Button button = new Button();

              
                    if (board.chesses[row, col] != null)
                    {
                        button.Background = Button_Background(board.chesses[row, col]);
                    }
                    else
                        button.Background = Brushes.Transparent; //button背景透明色

                    button.Click += new RoutedEventHandler(this.Button_Click);
                    button.SetValue(XQRowProperty, row);
                    button.SetValue(XQColProperty, col);
                    //button.Background = Brushes.Transparent; //button背景透明色
                    button.BorderBrush = Brushes.Transparent;//button边框透明
                    button.Width = 80;
                    button.Height = 80;

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    XiangqiGrid.Children.Add(button);
                    buttons.Add(button);//ButtonList
                }
            }
        }

        private ImageBrush  Button_Background(Chess chess)
        {
            ImageBrush brush = new ImageBrush();
            String source = "Resources/";

            if (chess.color == Color.red)
                source += "red";
            else
                source += "black";

            
            switch (chess.Print)
            {
                case "兵":
                   source += "Soilder.png";
                    break;
                case "炮":
                    source += "Cannon.png";
                    break;
                case "车":
                    source += "Rook.png";
                    break;
                case "马":
                    source += "Horse.png";
                    break;
                case "象":
                    source += "Elephant.png";
                    break;
                case "士":
                    source += "Guard.png";
                    break;
                case "将":
                    source += "King.png";
                    break;
            }
            brush.ImageSource = new BitmapImage(new Uri(source, UriKind.Relative));
            return brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int btnRow = (int)((Button)sender).GetValue(XQRowProperty);
            int btnCol = (int)((Button)sender).GetValue(XQColProperty);
           // MessageBox.Show("Button is: " + ((Button)sender).Content + "\n - Row    = " + btnRow + "\n - Column = " + btnCol );
            if(fail != Fail.no)
            {
                MessageBox.Show("The game is over!");
            } 
            else
                HandClick.HandleClick(btnRow,btnCol,sender);

            if (board.turn == Color.red)
                label1.Content = "红方";
            else if (board.turn == Color.black)
                label1.Content = "黑方";

            if(board.model == play_model.origin_chess)
            {
                label2.Content = "落棋";
            }
            else if (board.model == play_model.destination)
            {
                label2.Content = "选棋";
            }
        }


        public static  void Hint( int ROW, int COL ){
            for (int row = 0; row < 10; row++){          
                for (int col = 0; col < 9; col++){
                    if(board.chesses[row, col] == null || board.chesses[ROW, COL].color != board.chesses[row, col].color)
                    {
                        if (board.chesses[ROW, COL].CheckValidMove(row, col))
                        {
                            buttons[row * 9 + col].BorderBrush = Brushes.Black;
                        }
                    }
                }
            }
        }

        public static void CleanHint()
        {
            for (int row = 0; row < 10; row++){
                for (int col = 0; col < 9; col++){
                    if (buttons[row * 9 + col].BorderBrush == Brushes.Black)
                        buttons[row * 9 + col].BorderBrush = Brushes.Transparent;
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            CreateGrid();
        }
    }

   
}
