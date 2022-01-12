using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTeamXiangQi
{
    public class Board{
        public Chess[,] chesses;
        public play_model model;
        public Color turn = Color.red;
        
          
        public Board(){
            chesses = new Chess[10,9];
            model = play_model.origin_chess;
            //初始化
            for(int row = 0; row < 10; row++){
                for(int col = 0; col < 9 ; col++){
                   chesses[row,col] = null!;
                }
            }

            //红方
            chesses[0,0] = new Rook(0,0,Color.red,Type.Rook,this);
            chesses[0,1] = new Horse(0,1,Color.red,Type.Horse,this);
            chesses[0,2] = new Elephant(0,2,Color.red,Type.Elephant,this);
            chesses[0,3] = new Guard(0,3,Color.red,Type.Guard,this);
            chesses[0,4] = new King(0,4,Color.red,Type.King,this);
            chesses[0,5] = new Guard(0,5,Color.red,Type.Guard,this);
            chesses[0,6] = new Elephant(0,6,Color.red,Type.Elephant,this);
            chesses[0,7] = new Horse(0,7,Color.red,Type.Horse,this);
            chesses[0,8] = new Rook(0,8,Color.red,Type.Rook,this);
            chesses[2,1] = new Cannon(2,1,Color.red,Type.Cannon,this);
            chesses[2,7] = new Cannon(2,7,Color.red,Type.Cannon,this);
            chesses[3,0] = new Soldier(3,0,Color.red,Type.Soilder,this);
            chesses[3,2] = new Soldier(3,2,Color.red,Type.Soilder,this);
            chesses[3,4] = new Soldier(3,4,Color.red,Type.Soilder,this);
            chesses[3,6] = new Soldier(3,6,Color.red,Type.Soilder,this);
            chesses[3,8] = new Soldier(3,8,Color.red,Type.Soilder,this);

            //黑方
            chesses[9,0] = new Rook(9,0,Color.black,Type.Rook,this);
            chesses[9,1] = new Horse(9,1,Color.black,Type.Horse,this);
            chesses[9,2] = new Elephant(9,2,Color.black,Type.Elephant,this);
            chesses[9,3] = new Guard(9,3,Color.black,Type.Guard,this);
            chesses[9,4] = new King(9,4,Color.black,Type.King,this);
            chesses[9,5] = new Guard(9,5,Color.black,Type.Guard,this);
            chesses[9,6] = new Elephant(9,6,Color.black,Type.Elephant,this);
            chesses[9,7] = new Horse(9,7,Color.black,Type.Horse,this);
            chesses[9,8] = new Rook(9,8,Color.black,Type.Rook,this);
            chesses[7,1] = new Cannon(7,1,Color.black,Type.Cannon,this);
            chesses[7,7] = new Cannon(7,7,Color.black,Type.Cannon,this);
            chesses[6,0] = new Soldier(6,0,Color.black,Type.Soilder,this);
            chesses[6,2] = new Soldier(6,2,Color.black,Type.Soilder,this);
            chesses[6,4] = new Soldier(6,4,Color.black,Type.Soilder,this);
            chesses[6,6] = new Soldier(6,6,Color.black,Type.Soilder,this);
            chesses[6,8] = new Soldier(6,8,Color.black,Type.Soilder,this);
        }


        

        public void Turn(Color turn)
        {
            if (turn == Color.red)
            {
                this.turn = Color.black;
            }
            else
                this.turn = Color.red;
        }


        public void  selectChess(int row,int col)
        {
            if (this.chesses[row, col] == null)
                throw new Exception("There is not chess!");
            else if (this.chesses[row, col].color != this.turn)
                throw new Exception("You should choose " + this.turn.ToString() + " chess");

            model = play_model.destination;
        }

    }

    public abstract class Chess{
        public bool valid = false;
        protected Board board;
        public int row;
        public int col;
        public Color color;
        public Type type;
        public string Print;
 
        public Chess(int row, int col, Color color, Type type,string Print,Board board){
            this.row = row;
            this.col = col;
            this.color = color;
            this.type = type;
            this.Print = Print;
            this.board = board;
        }

        public void SetRow(int row){
            this.row = row;
        }

        public void SetCol(int col){
            this.col = col;
        }

        public void MoveChess(int start_row, int start_col ,int destination_row, int destination_col){
            this.SetRow(destination_row);
            this.SetCol(destination_col);
            this.board.chesses[destination_row,destination_col] = this;
            this.board.chesses[start_row,start_col] = null!;
        }

        public abstract bool CheckValidMove(int destinationColumn, int destinationRow);
    } 

   public class Soldier : Chess {
        public Soldier(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"兵",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){   
            bool other_side = false;
            valid = false;

            if(this.row < 5){ //红方地盘
                if(this.color == Color.black)  //黑棋
                other_side = true;
            }
            else{               //黑方地盘
                if(this.color == Color.red) 
                other_side = true;
            }
            if(Math.Abs(this.row - destinationRow) + Math.Abs(this.col -destinationColumn) == 1){  // 只走一格
                if(this.row == destinationRow){
                    if(other_side)
                        valid = true;
                }
                else if(this.row > destinationRow){
                    if(this.color == Color.black)
                        valid = true;
                }
                else{
                    if(this.color == Color.red)
                        valid = true;
                }

                /*if(valid == false){
                    if (other_side)
                        throw new Exception("You just can move forward or turn left or turn right.");
                    else*/
                        //throw new Exception("You just can move forward.");
                //}
            }
            else{
            valid = false;
                //throw new Exception("You just can move one space.");
            }
            return valid;                         
        }
    }
    public class Cannon : Chess {
        public Cannon(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"炮",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){  
            if(this.row == destinationRow || this.col == destinationColumn){//同一直线
                int countChess = 0;
                if(this.row != destinationRow ){         //同一直线下竖走
                    int RowMin = Math.Min(this.row,destinationRow);
                    int RowMax = Math.Max(this.row,destinationRow);
                    for(int Row = RowMin; Row <= RowMax; Row++){
                        if(board.chesses[Row,this.col] != null)
                            countChess++;          //有棋加1
                    }
                }

                if(this.col != destinationColumn){          //同一直线下竖走
                    int ColMin = Math.Min(this.col,destinationColumn);
                    int ColMax = Math.Max(this.col,destinationColumn);
                    for(int Col = ColMin; Col <= ColMax; Col++){
                        if(board.chesses[this.row,Col] != null)
                            countChess++;       //有棋加1
                    }
                }

                if(countChess == 1){        //一路无棋
                    valid = true;
                }
                else if(countChess == 3 && board.chesses[destinationRow,destinationColumn] != null){ // 路上有一棋 终点也有一棋
                        valid = true; 
                }
                else{
                    valid = false;
                    //throw new Exception("Your cannon is stuck.");
                }
            }
            else{
                valid = false;
                //throw new Exception("The cannon only can move straight or sideways.");
            }
            return valid;                            
        }
    }

    public class Rook : Chess {

        public Rook(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"车",board){}
         public override bool CheckValidMove(int destinationRow, int destinationColumn){     
            if(this.row != destinationRow && this.col != destinationColumn){//位置不在同一条线
                Console.WriteLine("Your Rook is unable to move.");
                valid = false;
            }
            else{//同一条线 
                int countChess = 0;//计数；
                    if(this.row == destinationRow){//横走                 
                        int ColMin =  Math.Min(destinationColumn,this.col);//最小量加变量，测试路上有无棋子阻碍
                        int ColMax =  Math.Max(destinationColumn,this.col);
                        for(int Col = ColMin+1; Col < ColMax; Col++){
                            if(board.chesses[this.row,Col] != null ){
                                countChess++;
                            }
                        }     
                    }
                    else if(this.col == destinationColumn){//竖走
                        int RowMin =  Math.Min(destinationRow,this.row);
                        int RowMax =  Math.Max(destinationRow,this.row);
                        for(int Row = RowMin+1; Row < RowMax ; Row++){
                            if(board.chesses[Row,this.col] != null ){
                                countChess++; 
                            }
                        }     
                    }

                if( countChess == 0 ){//一路上没有棋
                    valid = true; 
                }
                else{//其他情况，不能走
                    valid = false;
                    //throw new Exception("Your Rook is stuck.");
                }
            }
            return valid;                          
        }
    }

    public class Horse : Chess {

        public Horse(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"马",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){  

            int rowAbs = Math.Abs(destinationRow-this.row);
            int colAbs = Math.Abs(destinationColumn-this.col);
            
            if( rowAbs == 1 && colAbs == 2){ //横着走
                if( board.chesses[this.row,(this.col+destinationColumn)/2] != null){ //有棋
                    valid = false;
                    //throw new Exception("Your Horse is stuck.");
                }   
                else{
                    valid = true;                            
                }
            }
            else if(colAbs==1 && rowAbs == 2){ //竖着走
                if( board.chesses[(this.row+destinationRow)/2,this.col] != null){ //有棋
                    valid = false;
                    //throw new Exception("Your Horse is stuck.");    
                }
                else{
                    valid = true;
                }
            } 
            else{
                valid = false;
                //throw new Exception("You should walk as a 日.");                 
            }//红黑马end
            return valid;                            
        }
    }

    public class Elephant : Chess {

        public Elephant(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"象",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){  
            int rowAbs = Math.Abs(destinationRow-this.row);
            int colAbs = Math.Abs(destinationColumn-this.col); 
            if(rowAbs == 2 && colAbs == 2){//符合两格
                if((destinationRow>4 && board.chesses[row,col].color == Color.red) //红象过河
                  || (destinationRow<5 && board.chesses[row,col].color == Color.black)){//黑象过河
                    valid = false;
                    //throw new Exception("Your elephant can't cross the river.");
                }
                else{
                    if(rowAbs == 2 && colAbs == 2){//符合两格
                                
                        if(board.chesses[(this.row+destinationRow)/2,(this.col+destinationColumn)/2] != null){//绊脚
                                valid = false;
                            //throw new Exception("Your elephant is stuck.");
                        }
                        else { valid = true; }
                    }                
                }
            }
            else{
                valid = false;
                //throw new Exception("The elepant only can move two squares.");
            }
            return valid;                            
        }
    }

    public class Guard : Chess {
        public Guard(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"士",board){}
             public override bool CheckValidMove(int destinationRow, int destinationColumn){
                int rowAbs = Math.Abs(this.row - destinationRow);
                int colAbs = Math.Abs(this.col - destinationColumn);

                if((destinationColumn > 2) && (destinationColumn < 6)){    // Col 限制 【3，5】
                    if(this.row != destinationRow && this.col != destinationColumn){
                        if( rowAbs + colAbs == 2){
                            valid = true;
                        }
                        else{
                            valid = false;
                            //throw new Exception("You only can walk diagonally one space.");
                    }
                    }
                    else{
                        valid = false;
                        //throw new Exception("Sorry, you have to walk diagonally.");
                }
                }
                else{
                    valid = false;
                    //throw new Exception("Sorry, you only can move in the 米 space.");
                }          
                return valid;                              
            }
    }

    public class King : Chess {

        public King(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"将",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){
            bool king_flag  = false;  
            if(board.chesses[destinationRow,destinationColumn] == null){ //目标为空，不进入将吃将
                king_flag = false;
            }
            else{ 
                if(board.chesses[destinationRow,destinationColumn].type == Type.King) //目标为将，进入将吃将
                    king_flag = true;
                else //目标不为将，不进入将吃将
                    king_flag = false;
            }

            if(king_flag){  // 将吃将（目标为将）
                if(this.col == destinationColumn){//两将同一列
                    int RowMin = Math.Min(this.row,destinationRow);
                    int RowMax = Math.Max(this.row,destinationRow);
                    for(int Row = RowMin+1; Row < RowMax ; Row++){
                        if(board.chesses[Row,this.col] == null) //路上无棋
                            valid = true;
                        else
                            valid = false;
                        if( valid == false){// 有棋退出
                            //throw new Exception("Sorry, you only can not eat the King.There is at least one chess between two Kings.");
                        }                                   
                    }
                }
                else{//两将不同列
                    valid = false;
                    //throw new Exception("Sorry, you can not eat the King. You are not in same coloumn.");
                }
            }
            else{ //在米格里
            int AbsRowAndCol =  Math.Abs(this.row - destinationRow) + Math.Abs(this.col - destinationColumn);
                if(destinationColumn > 2 && destinationColumn < 6){    // 米格 Col ： [3,5]
                    if( AbsRowAndCol == 1 )
                        valid = true;
                    else{
                        valid = false;
                        //throw new Exception("Sorry, you only can walk one space.");
                    }
                }
                else{
                    valid = false;
                    //throw new Exception("Sorry, you only can move in the 米 space.");
                }
            }
            return valid;                            
        } 
    }

    public enum Color{red, black}
    public enum Type{King, Guard, Elephant, Horse, Rook, Cannon, Soilder}
    public enum Fail{red, black, no}
    public enum play_model {origin_chess, destination }

  
}
