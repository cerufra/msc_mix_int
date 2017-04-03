
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package angry;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.math.BigDecimal;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Random;
import java.util.Scanner;
import java.util.TreeMap;

/**
 *
 * @author CERUTINHO
 */
public class Angry {

   public static char[][] Circle = {{0,0,'w','w','w',0,0},
                                   {0,'a','a','a','a','a',0},
                                {'a','a', 'a', 'a', 'a','a','a'},
                                {'a','a', 'a', 'a', 'a','a','a'},
                                {'a','a', 'a', 'a', 'a','a','a'},
                                {'a','a', 'a', 'a', 'a','a','a'},
                                {0,'a', 'a', 'a', 'a','a',0},
                                    {0,0,'a','a','a',0,0}};
    public static char[][] CircleSmall = {{0, 'w', 0},
    {'b', 'b', 'b'},
    {'b', 'b', 'b'},
      {0, 'b', 0}};
    public static char[][] RectBigLying = {{'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w'},
                                           {'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c'}};
    public static char[][] RectBigStanding = {{'w', 'w'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'},
    {'d', 'd'}};
    public static char[][] RectFatLying = {{'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w'},
                                           {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                                           {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                                           {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'}};

    public static char[][] RectFatStanding = {{'w', 'w', 'w', 'w'},
                                              {'f', 'f', 'f', 'f'},
                                              {'f', 'f', 'f', 'f'},
                                              {'f', 'f', 'f', 'f'},
                                              {'f', 'f', 'f', 'f'},
                                              {'f', 'f', 'f', 'f'},
                                              {'f', 'f', 'f', 'f'},
                                              {'f', 'f', 'f', 'f'}};
    public static char[][] RectMediumLying = {{'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w','w'},
                                              {'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g','g'}};
    public static char[][] RectMediumStanding = {{'w', 'w'},
    {'h', 'h'},
    {'h', 'h'},
       {'h', 'h'},
    {'h', 'h'},
       {'h', 'h'},
    {'h', 'h'},
       {'h', 'h'},
    {'h', 'h'},
       {'h', 'h'},
    {'h', 'h'},
       {'h', 'h'},
    {'h', 'h'},
       {'h', 'h'},
    {'h', 'h'},
       {'h', 'h'},
    {'h', 'h'}};
    public static char[][] RectSmallLying = {{'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w'},
                                             {'j', 'j', 'j', 'j', 'j', 'j', 'j', 'j', 'j'}};
    
    public static char[][] RectSmallStanding = {{'w', 'w'},
    {'k', 'k'},
    {'k', 'k'},
    {'k', 'k'},
    {'k', 'k'},
    {'k', 'k'},
    {'k', 'k'},
    {'k', 'k'}};
    public static char[][] RectTinyLying = {{'w', 'w','w', 'w'},
   {'i', 'i','i', 'i'} };
    public static char[][] RectTinyStanding = {{'w','w'},
                                               {'m','m'},
                                               {'m','m'},
                                               {'m','m'}};
    
    public static char[][] SquareHole = {{'w', 'w', 'w', 'w','w', 'w', 'w', 'w'},
                                         {'n', 'n', 'n', 'n','n', 'n', 'n', 'n'},
                                         {'n', 'n', 'n', 'n','n', 'n', 'n', 'n'},
                                         {'n', 'n', 'n', 'n','n', 'n', 'n', 'n'},
                                         {'n', 'n', 'n', 'n','n', 'n', 'n', 'n'},
                                         {'n', 'n', 'n', 'n','n', 'n', 'n', 'n'},
                                         {'n', 'n', 'n', 'n','n', 'n', 'n', 'n'},
                                         {'n', 'n', 'n', 'n','n', 'n', 'n', 'n'}};
    public static char[][] SquareSmall = {{'w', 'w', 'w', 'w'},
    {'p', 'p', 'p', 'p'},
    {'p', 'p', 'p', 'p'},
    {'p', 'p', 'p', 'p'}};
    public static char[][] SquareTiny = {{'w', 'w'},
    {'q', 'q'}};
    public static char[][] Triangle = {{'w',0,0,0,0,0,0,0},
                                       {'r', 'r', 0, 0,0,0,0,0},
                                       {'r', 'r', 'r', 0,0,0,0,0},
                                       {'r', 'r', 'r', 'r',0,0,0,0},
                                       {'r', 'r', 'r', 'r','r',0,0,0},
                                       {'r', 'r', 'r', 'r','r',0,0,0},
                                       {'r', 'r', 'r', 'r','r','r',0,0},
                                       {'r', 'r', 'r', 'r','r','r','r',0},
                                       {'r', 'r', 'r', 'r','r','r','r','r'}};
    public static char[][] TriangleMirror = {{0,0,0,0,0,0,0,'w'},
                                          {0,0,0,0,0,0,'s', 's'},
                                      {0,0,0,0,0, 's', 's', 's'},
                                    {0,0,0,0,'s', 's', 's', 's'},
                                  {0,0,0,'s','s', 's', 's', 's'},
                                {0,0,'s','s','s', 's', 's', 's'},
                                {0,'s','s','s','s', 's', 's', 's'},
                             {'s','s','s','s','s', 's', 's', 's'}};
    public static char[][] TriangleHole = {{0,0, 0, 0, 'w', 0, 0, 0,0},
                                           {0,0, 0, 't', 't', 't', 0,0,0},
                                            {0,0, 0, 't', 't', 't', 0,0,0},
                                         {0,0, 't', 't', 't', 't', 't', 0,0},
                                         {0,0, 't', 't', 't', 't', 't', 0,0},
                                        {0,'t', 't', 't', 't', 't', 't','t',0},
                                       {0,'t', 't', 't', 't', 't', 't', 't',0},
                                     {'t','t','t','t', 't', 't', 't', 't', 't'}};

    public static char[][] cenario;
    public static String[] materiais;

    public static char[][] teste;
    public static String[][] bloco;
    public static int[][] blocos;
    public static int[][] scenario;
    public static BigDecimal[][] coordenadas;
    public static BigDecimal big;
    //public static String[][] blocousado; 
    public static Map<Integer, String> blockmap = new HashMap<>();
    public static TreeMap<Integer, Integer> blockusado = new TreeMap<>();
    public static TreeMap<Integer, Integer> blockusado2 = new TreeMap<>();
    public static TreeMap<Integer, Integer> blockusadoy = new TreeMap<>();
    public static TreeMap<Integer, Integer> blockusadoy2 = new TreeMap<>();
    public static int m;
    public static int n;
    public static int z;
    public static int somaaux;
    public static int melhorsoma;
    public static int somaguloso;
    public static int posx;
    public static int posy;
    public static int posty;
    public static int postx;
    public static int dx;
    public static int dy;
    public static int eixox;
    public static int eixoy;
    public static BigDecimal descretizacao;
    public static int qtde;
    public static int bloqueado;
    public static BigDecimal chao;// y minimo ou seja coordenadas y do chao nao vaira 
    public static BigDecimal lado; //xminimo varia de a cordo com o cenario do unity por em quanto e 0 
    public static BigDecimal meiox;
    public static BigDecimal meioy;
    public static BigDecimal totalx;
    public static BigDecimal totaly;
    public static BigDecimal truex;
    public static BigDecimal truey;
    public static BigDecimal rotacao;
    public static String material;
    public static String nomebloco;
    public static String cadeia;

    /**
     * @param args the command line arguments
     * @throws java.io.FileNotFoundException
     * C:\\Users\\dpime\\OneDrive\\Documentos\\ScienceBirds-master\\Assets\\Resources\\Levels
     */
// TODO code application logic here RectMediumLying ,
    public static void main(String[] args) throws FileNotFoundException, IOException {

        //pegando o arquivo block que tem as informacoes sobre os blocos 
        File angry = new File("C:\\Users\\dpime\\OneDrive\\Documentos\\GitHub\\msc_mix_int_java\\src\\dataset\\block");
        //File angry = new File("C:\\Users\\CERUTINHO\\Documents\\GitHub\\msc_mix_int_java\\src\\dataset\\block");
       
  
        FileReader angried = new FileReader(angry);
        //lendo o arquivo filereder
        try (BufferedReader input = new BufferedReader(angried)) {

            Scanner sc = new Scanner(angry);
            dx = Integer.parseInt(sc.nextLine());
            dy = Integer.parseInt(sc.nextLine());
            qtde = Integer.parseInt(sc.nextLine());
            descretizacao = new BigDecimal(sc.nextLine());
            chao = new BigDecimal(sc.nextLine());
            lado = new BigDecimal(sc.nextLine());
            bloco = new String[qtde][11];//matriz onde tera os dados dos blocos 
            for (int i = 0; i < qtde; i++) {
                String parts[] = sc.nextLine().split(",");
                //ordem do bloco 
                String ord = parts[0];
                // largura do bloco x
                String x = parts[1];
                //altura do bloco y
                String y = parts[2];
                //total que ocupa a peça 
                String g = parts[3];
                //nome da peça
                String h = parts[4];
                //largura do bloco no Unity truex
                String tx = parts[5];
                //altura do bloco  truey 
                String ty = parts[6];
                //rotaçao 0 deitado 90 em pe       
                String rota = parts[7];
                //id do bloco       
                String id = parts[8];
                //casas decimais para o eixo X       
                String ex = parts[9];
                //casas decimais para o eixo Y       
                String ey = parts[10];
                bloco[i][0] = ord;
                bloco[i][1] = x;
                bloco[i][2] = y;
                bloco[i][3] = g;
                bloco[i][4] = h;
                bloco[i][5] = tx;
                bloco[i][6] = ty;
                bloco[i][7] = rota;
                bloco[i][8] = id;
                bloco[i][9] = ex;
                bloco[i][10] = ey;

            }
        }
        materiais = new String[3];
        materiais[0] = "\"wood\"";
        materiais[1] = "\"stone\"";
        materiais[2] = "\"ice\"";

        posty = dy - 1;
        postx = dx - 1;
        blocos = new int[200][7];
        scenario = new int[dy][dx];
        cenario = new char[dy][dx];
        coordenadas = new BigDecimal[200][5];

        for (int l = 1; l < 200; l++) {
            //sorteio de um bloco para inserir na matris 
            Random s = new Random();
            int wii = Math.abs(s.nextInt()) % qtde + 1;
            Random sa = new Random();
            int wha = Math.abs(sa.nextInt()) % 3 + 1;
            String o;
            material = materiais[wha - 1];
            z = Integer.valueOf(bloco[wii - 1][8]);
            m = 0;
            n = 0;
            m = Integer.valueOf(bloco[wii - 1][2]);
            n = Integer.valueOf(bloco[wii - 1][1]);
            truex = new BigDecimal(bloco[wii - 1][5]);
            truey = new BigDecimal(bloco[wii - 1][6]);
            nomebloco = (bloco[wii - 1][4]);
            rotacao = new BigDecimal(bloco[wii - 1][7]);
            somaaux = Integer.valueOf(bloco[wii - 1][3]);
            eixox = Integer.valueOf(bloco[wii - 1][9]);
            eixoy = Integer.valueOf(bloco[wii - 1][10]);
            teste = new char[m][n];
            switch (z) {
                case 1:

                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = Circle[i][j];
                        }
                    }
                    for (int d = 0; d < m; d++) {
                        for (int a = 0; a < n; a++) {

                        }

                    }
                    break;
                case 2:

                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = CircleSmall[i][j];
                        }
                    }
                    break;
                case 3:

                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectBigLying[i][j];
                        }
                    }
                    break;
                case 4:

                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectBigStanding[i][j];
                        }
                    }
                    break;
                case 5:

                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectFatLying[i][j];
                        }
                    }
                    break;
                case 6:

                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectFatStanding[i][j];
                        }
                    }
                    break;
                case 7:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectMediumLying[i][j];
                        }
                    }
                    break;
                case 8:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectMediumStanding[i][j];
                        }
                    }
                    break;
                case 9:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectSmallLying[i][j];
                        }
                    }
                    break;
                case 10:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectSmallStanding[i][j];
                        }
                    }
                    break;
                case 11:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectTinyLying[i][j];
                        }
                    }
                    break;
                case 12:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = RectTinyStanding[i][j];
                        }
                    }
                    break;
                case 13:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = SquareHole[i][j];
                        }
                    }
                    break;
                case 14:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = SquareSmall[i][j];
                        }
                    }
                    break;

                case 15:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {

                            teste[i][j] = SquareTiny[i][j];
                        }
                    }
                    break;
                case 16:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {
                            teste[i][j] = Triangle[i][j];
                        }
                    }
                    break;
                case 17:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {
                            teste[i][j] = TriangleMirror[i][j];
                        }
                    }
                default:
                    for (int i = 0; i < m; ++i) {
                        for (int j = 0; j < n; ++j) {
                            teste[i][j] = TriangleHole[i][j];
                        }
                    }

            }

            for (int i = 0; i < dy; ++i) {
                for (int j = 0; j < dx; ++j) {
                    posy = posty - i;
                    posx = j;
                    int mm = posty - m - i;
                    int nn = n + j;
                    if (nn >= dx) {
                        break;
                    }
                    if (mm < 0) {
                        break;
                    }
                    char as = cenario[posy][posx];
                    char bs = cenario[mm + 1][posx];
                    char cs = cenario[posy][nn - 1];
                    char ds = cenario[mm + 1][nn - 1];

                    //   System.out.println(" -> posy: " + posy + " -> posx: " + posx + " -> mm: " + mm + " -> nn: " + nn);
                    if (as == 0 && bs == 0 && cs == 0 && ds == 0) {
                        char p1, p2;
                        for (int ti = 0; ti < n; ++ti) {
                            int xx1 = posx + ti;
                            p1 = cenario[posy][xx1];
                            p2 = cenario[mm + 1][xx1];
                            if (p1 != 0 || p2 != 0) {
                                bloqueado = bloqueado + 1;
                            }
                        }
                        p1 = 0;
                        p2 = 0;
                        for (int ti = 0; ti < m; ++ti) {
                            int yy1 = posy - ti;
                            p1 = cenario[yy1][posx];
                            p2 = cenario[yy1][nn - 1];
                            if (p1 != 0 || p2 != 0) {
                                bloqueado = bloqueado + 1;
                            }
                        }

                        String caden;
                        if (posy != posty) {
                            int pto1, pto2, pto, pto3;
                            char pt1, pt2, pt3;

                            pto1 = posy + 1;
                            //   System.out.println(" -> pto1: " + pto1);

                            pto = Math.round(n / 2);
                            pto3 = posx + pto;

                            // System.out.println(" -> pto2: " + pto2);
                            pt1 = cenario[pto1][posx];
                            //   System.out.println(" -> pt1: " + pt1);

                            pt2 = cenario[pto1][nn - 1];

                            pt3 = cenario[pto1][pto3];

                            // 
                            // 
                            //   System.out.println(" -> pt2: " + pt2);
                            if (pt1 != 0 && pt2 != 0 && pt3 != 0) {

                                if (bloqueado == 0) {
                                    int chave = 0;
                                    if (blockusado.isEmpty()) {
                                        chave = 1;

                                    } else {
                                        Integer key = blockusado.lastKey();

                                        chave = key + 1;
                                    }
                                    if (posx != postx) {
                                        BigDecimal bd;
                                        bd = new BigDecimal(posx);
                                        totalx = descretizacao.multiply(bd);

                                    } else {
                                        int pan;
                                        char pa = cenario[posy][posx - 1];
                                        if (pa != 'x' && pa != 0) {
                                            pan = scenario[posy][posx - 1];
                                            totalx = coordenadas[pan - 1][0];
                                        } else {
                                            totalx = new BigDecimal(0);
                                        }
                                    }
                                    int pin;

                                    pin = scenario[pto1][posx];
                                 

                                    if (pt1 != 'x' && pt1 != 0) {
                                        totaly = coordenadas[pin - 1][1];
                                         System.out.println("totaly : no primeiro" + totaly);
                                    }  else {

                                        totaly = chao;
                                    }

                                    BigDecimal meix, meiy;
                                    meix = truex.divide(new BigDecimal("2"));

                                    meiy = truey.divide(new BigDecimal("2"));

                                    for (int ii = 0; ii < m; ++ii) {
                                        for (int jj = 0; jj < n; ++jj) {
                                            int yy = posy - ii;
                                            int xx = posx + jj;
                                            int tt = m - ii;
                                            cenario[yy][xx] = teste[tt - 1][jj];
                                            scenario[yy][xx] = chave;
                                        }
                                    }
                                    cenario[pto1][posx] = 'a';
                                    //   System.out.println(" -> pt1: " + pt1);

                                    cenario[pto1][nn - 1] = 'b';

                                    cenario[pto1][pto3] = 't';
                                    blockusado.put(chave, z);
                                    somaguloso = somaaux + somaguloso;
                                    blocos[chave - 1][0] = z;
                                    i = dy;
                                    j = dx;
                                    truex=truex.add(totalx);
                                    truex = truex.setScale(eixox, BigDecimal.ROUND_HALF_EVEN);
                                    truey=truey.add(totaly);
                                    truey = truey.setScale(eixoy, BigDecimal.ROUND_HALF_EVEN);
                                    coordenadas[chave - 1][0] = truex;
                                    coordenadas[chave - 1][1] = truey;
                                    meiox = meix.add(totalx);
                                    meioy = meiy.add(totaly);
                                    meiox = meiox.setScale(eixox, BigDecimal.ROUND_HALF_EVEN);
                                    meioy = meioy.setScale(eixoy, BigDecimal.ROUND_HALF_EVEN);
                                     System.out.println("meioy na primeira : " + meioy); 
                                    caden = "\n" + "<Block type=" + nomebloco + " material=" + "\"wood\"" + " x=\"" + meiox + "\" y=\"" + meioy + "\" rotation=\"" + rotacao + "\" />";
                                    cadeia = cadeia + caden;
                                    break;

                                }
                                //final do bloqueado 
                            } else {
                                bloqueado = bloqueado + 1;
                                break;
                            }
                        } else if (bloqueado == 0) {

                            int chave = 0;
                            if (blockusado.isEmpty()) {
                                chave = 1;

                            } else {

                                Integer key = blockusado.lastKey();

                                chave = key + 1;
                            }

                            if (posx != postx) {

                                BigDecimal bd;
                                bd = new BigDecimal(posx);
                                totalx = descretizacao.multiply(bd);

                            } else {
                                int pan;
                                char pa = cenario[posy][posx - 1];
                                if (pa != 'x' && pa != 0) {
                                    pan = scenario[posy][posx - 1];
                                    totalx = coordenadas[pan - 1][0];
                                } else {
                                    BigDecimal bd;
                                    bd = new BigDecimal(posx);
                                    totalx = descretizacao.multiply(bd);
                                }
                            }

                            totaly = chao;
                            BigDecimal meix, meiy;
                            meix = truex.divide(new BigDecimal("2"));

                            meiy = truey.divide(new BigDecimal("2"));

                            for (int ii = 0; ii < m; ++ii) {
                                for (int jj = 0; jj < n; ++jj) {
                                    int yy = posy - ii;
                                    int xx = posx + jj;
                                    int tt = m - ii;
                                    cenario[yy][xx] = teste[tt - 1][jj];
                                    scenario[yy][xx] = chave;
                                }
                            }

                            blockusado.put(chave, z);
                            somaguloso = somaaux + somaguloso;
                            blocos[chave - 1][0] = z;
                            truex=truex.add(totalx);
                            truex = truex.setScale(eixox, BigDecimal.ROUND_HALF_EVEN);
                            truey=truey.add(totaly);
                            truey = truey.setScale(eixoy, BigDecimal.ROUND_HALF_EVEN);
                            coordenadas[chave - 1][0] = truex;
                            coordenadas[chave - 1][1] = truey;
                            meiox = meix.add(totalx);
                            meioy = meiy.add(totaly);
                            meiox = meiox.setScale(eixox, BigDecimal.ROUND_HALF_EVEN);
                            meioy = meioy.setScale(eixoy, BigDecimal.ROUND_HALF_EVEN);
                            System.out.println("meioy na segunda : " + meioy);
                            i = dy;
                            j = dx;
                            String cade = "\n" + "<Block type=" + nomebloco + " material=" + "\"wood\"" + " x=\"" + meiox + "\" y=\"" + meioy + "\" rotation=\"" + rotacao + "\" />";
                            cadeia = cade + cadeia;
                            break;

                        }
                    }

                    bloqueado = 0;

                }
            }

            bloqueado = 0;

        }

        //final
        //aqui final    
        System.out.println("-------------------------Resultado Guloso------------------------------------------");
        System.out.printf("matriz final");
        System.out.printf("\n");
        for (int d = 0; d < dy; d++) {
            for (int a = 0; a < dx; a++) {
                System.out.printf("%c\t", cenario[d][a]);
            }
            System.out.printf("\n");

        }
        System.out.printf("\n");
        System.out.printf("matriz posiçao");
        System.out.printf("\n");
        for (int d = 0; d < dy; d++) {
            for (int a = 0; a < dx; a++) {
                System.out.printf("%d\t", scenario[d][a]);
            }
            System.out.printf("\n");

        }
                for (int d = 0; d < 5; d++) {
        System.out.println("total x"+coordenadas[d][0]);  
        System.out.println("total y"+coordenadas[d][1]); 
System.out.printf("\n");
      
            

        }
        Iterator it;
        it = blockusado.keySet().iterator();

        while (it.hasNext()) {
            Integer key = (Integer) it.next();
            Integer valor = blockusado.get(key);
            System.out.println("Clave: " + key + " -> bloco usado : " + valor);
        }
        System.out.println("Total de espaco utilizado: " + somaguloso);
        melhorsoma = somaguloso;
        blockusadoy = (TreeMap) blockusado.clone();

        String testString = "";
        testString = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + "\n" +"<Level width=\"2\">" + "\n" +"<Camera x=\"0\" y=\"-1\">" +" minWidth=\"15\" maxWidth=\"17.5\">"+"\n"+"<Birds>"+"\n"+"<Bird type=\"BirdBlue\"/>"+ "\n" +"<Bird type=\"BirdBlack\"/>"+"\n"+"<Bird type=\"BirdRed\"/>"+"\n"
 + "</Birds>" + "\n" + "<Slingshot x=\"-8.5\" y=\"-2.5\">" + "\n" + "<GameObjects>" + cadeia + "\n" + "</GameObjects>" + "\n" + "</Level>";
        //StringBuilder str = new StringBuilder();
        //str.append("texto texto");
        try {
           FileWriter out = new FileWriter("C:\\Users\\dpime\\OneDrive\\Documentos\\GitHub\\msc_mix_int_java\\src\\dataset\\level-1.xml");
           // FileWriter outi = new FileWriter("C:\\Users\\dpime\\OneDrive\\Documentos\\ScienceBirds-master\\Assets\\Resources\\Levels\\level-1.xml");

           // FileWriter out = new FileWriter("C:\\Users\\CERUTINHO\\Documents\\NetBeansProjects\\msc_mix_int_java\src\\dataset\\level-1.xml");
          //  FileWriter outi = new FileWriter("C:\\Users\\CERUTINHO\\Downloads\\ScienceBirds-master\\ScienceBirds-master\\Assets\\Resources\\Levels\\level-1.xml");

            out.write(testString);
            out.close();
          //  outi.write(testString);
         //   outi.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println("resultado cadeia: " + cadeia);

    }
}
        /// busca local 
