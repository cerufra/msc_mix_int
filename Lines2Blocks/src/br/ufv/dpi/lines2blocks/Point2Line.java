/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.ufv.dpi.lines2blocks;

/**
 *
 * @author dpimestrado
 */
import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Scanner;
import br.ufv.dpi.blockscombinations.Block;
import java.io.FileWriter;
import java.io.IOException;
import java.math.BigDecimal;
import java.util.TreeMap;

public class Point2Line {

    private ArrayList<Line> linesList = new ArrayList<Line>();
    private ArrayList<Sequence> sequenceblock = new ArrayList<Sequence>();
    public static TreeMap<Integer, String> scriptmap = new TreeMap<>();
    private MapDistBlocks map;
    private String script2;
    private String script3;
    private BigDecimal floor;// Its the minimal y axis in the scene
    private BigDecimal discretization;// transform the x axis in the unity x axis
    private FileWriter out;
    private char scenery[][] = new char[100][100];
    private int position[][] = new int[100][100];
    private int count;
    private char check;

    public Point2Line(String filename) {
        map = new MapDistBlocks(filename);
    }

    public void readPoint(String filename) throws FileNotFoundException {
        try {
            File file = new File(filename);
            Scanner scanner = new Scanner(file);
            floor = new BigDecimal(-3.436699);
            discretization = new BigDecimal(0.0942);
            while (scanner.hasNextLine()) {
                String points[] = scanner.nextLine().split(",");
                System.out.println(" >> " + points[0]);
                int x1 = Integer.parseInt(points[0]);
                int y1 = Integer.parseInt(points[1]);
                int x2 = Integer.parseInt(points[2]);
                int y2 = Integer.parseInt(points[3]);
                System.out.println("x1: " + x1);
                System.out.println("y1: " + y1);
                System.out.println("x2: " + x2);
                System.out.println("y2: " + y2);
                String material = points[4];
                Point p1 = new Point(x1, y1);
                Point p2 = new Point(x2, y2);
                Line line = new Line(p1, p2, material);
                linesList.add(line);
                this.computeDistance(line);
            }

            scanner.close();

        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

    public void computeDistance(Line line) {
        Point p1 = line.getP1();
        Point p2 = line.getP2();
        String material = line.getMaterial();
        double diffX = p2.getPx() - p1.getPx();
        double diffY = p2.getPy() - p1.getPy();
        double auxiliardegree = diffY / diffX;
        double degree = Math.ceil(Math.toDegrees((Math.atan(auxiliardegree))));
        if (degree <= 0) {
            degree = 180 + degree;
        }
        int distance = (int) Math.round(Math.sqrt(Math.pow((p2.getPx() - p1.getPx()), 2) + Math.pow(((p2.getPy()) - p1.getPy()), 2)));
        System.out.println("Distancia antiga: " + distance);
        System.out.println("p1.getPx(): " + p1.getPx());
        System.out.println("p2.getPy(): " + p1.getPy());
        System.out.println("p2.getPx() " + p2.getPx());
        System.out.println("p2.getPy() " + p2.getPy());
        int firstpointx = p1.getPx(); //p1.getPx()
        int firstpointy = p1.getPy(); //p1.getPy()
        int lastpointx = p2.getPx();
        int lastpointy = p2.getPy();
        int unknowpointx = 0;
        int unknowpointy = 0;
        int distancenew = distance;
        int height = 0;
        if (degree == 135) {
            firstpointx = p2.getPx();
            firstpointy = p2.getPy();
            lastpointx = p1.getPx();
            lastpointy = p1.getPy();
        }
        if (degree == 180) {
            if (firstpointy == 0) 
            {
                System.out.println("aqui entrou pq fistpointy e 0 ");
                firstpointx = p1.getPx() + 1;
                lastpointx = p2.getPx() - 1;
                System.out.println("old distance : " + distance);
                distance = (int) Math.round(Math.sqrt(Math.pow((lastpointx - firstpointx), 2) + Math.pow((lastpointy - firstpointy), 2)));
                System.out.println("180 new distance : " + distance);
                distancenew = distance;
                
            }
           
            degree = 0;
        }
        if (degree == 90) {
            if (lastpointy < firstpointy) {
                firstpointx = p2.getPx();
                firstpointy = p2.getPy();
                lastpointx = p1.getPx();
                lastpointy = p1.getPy();
            }
        }
        if (degree != 0) {
            lastpointy = lastpointy - 1;
            System.out.println("45 old distance : " + distance);
            distance = (int) Math.round(Math.sqrt(Math.pow((lastpointx - firstpointx), 2) + Math.pow((lastpointy - firstpointy), 2)));
            System.out.println("45 new distance : " + distance);
            distancenew = distance;
           /* else{
                lastpointy = lastpointy - 1;
            System.out.println("45 old distance : " + distance);
            distance = (int) Math.round(Math.sqrt(Math.pow((lastpointx - firstpointx), 2) + Math.pow((lastpointy - firstpointy), 2)));
            System.out.println("45 new distance : " + distance);
            distancenew = distance;
            }*/
        }
        System.out.println("firstpointx : " + firstpointx);
        System.out.println("firstpointy : " + firstpointy);  
        System.out.println("lastpointy : " + lastpointx);
        System.out.println("lastpointy : " + lastpointy); 
        System.out.println("degree : " + degree); 
        int distancePointsMap = 0;
            System.out.println("distance entrando : " + distance);
        if (map.containsKey((Integer) distance)) {
            System.out.println("distancePointsMap : " + distance);
            distancePointsMap = distance;
        } else {
            distancePointsMap = map.getClosest((Integer) distance);
             System.out.println("distancePointsMap else : " + map.getClosest((Integer) distance));
        }
        ArrayList<Block> list = map.getBlockList(distancePointsMap).getList();
        int midpointx = 0;
        int midpointy = 0;
        unknowpointx = 0;
        unknowpointy = 0;
        BigDecimal unityx;
        BigDecimal unityy;
        BigDecimal auxunityy;
        BigDecimal unityynew;
        int keymap = 0;
        if ((degree == 90) || (degree == 0)) {
               System.out.println("firstpointx 90 ou 0: " + firstpointx);
               System.out.println("firstpointy 90 ou 0: : " + firstpointy);  
               System.out.println("lastpointy 90 ou 0:: " + lastpointx);
               System.out.println("lastpointy 90 ou 0:: " + lastpointy); 
               System.out.println("distance 90 ou 0:: " + distance); 
               for (Iterator<Block> iterator = list.iterator(); iterator.hasNext();) {
                Block block = (Block) iterator.next();
                String name = block.getName();
                height = block.getHeight();
                BigDecimal fit = new BigDecimal("0.2");
                double r = (double) (height) / (double) (distancenew);
                unknowpointx = (int) (firstpointx + r * (lastpointx - firstpointx));
                unknowpointy = (int) (firstpointy + r * (lastpointy - firstpointy));
                midpointx = (int) ((firstpointx + unknowpointx) / 2);
                midpointy = (int) ((firstpointy + unknowpointy) / 2);
                System.out.println("peças: " + name);
                System.out.println("tamanho da peças: " + height);
                unityx = discretization.multiply(new BigDecimal(midpointx));
                unityx = unityx.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                auxunityy = discretization.multiply(new BigDecimal(midpointy));
                unityy = floor.add(auxunityy);
                unityy = unityy.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                String auxiliaryscript = "\n" + "<Block type=" + "\"" + name + "\"" + " material=" + material + " x=\"" + unityx + "\" y=\"" + unityy + "\" rotation=\"" + degree + "\" />";
                if (scriptmap.isEmpty()) {
                    keymap = 1;
                } else {
                    keymap = scriptmap.lastKey() + 1;
                }
                script3 = script3 + auxiliaryscript;

                if (degree == 90) {
                    for (int i = firstpointy; i <= unknowpointy + 1; ++i) {
                        int j = firstpointx;
                        scenery[i][j] = 'x';
                        scenery[i][j + 1] = 'x';
                        position[i][j] = keymap;
                        position[i][j + 1] = keymap;
                    }
                }
                if (degree == 0) {
                    for (int t = firstpointx; t <= unknowpointx; ++t) {
                        int u = firstpointy;
                        scenery[u][t] = 'a';
                        scenery[u + 1][t] = 'a';
                        position[u][t] = keymap;
                        position[u + 1][t] = keymap;
                    }
                }

                scriptmap.put(keymap, auxiliaryscript);
                Sequence sequence = new Sequence(name, firstpointx, firstpointy, unknowpointx, unknowpointy, degree);
                sequenceblock.add(sequence);
                firstpointx = unknowpointx;
                firstpointy = unknowpointy;
                distancenew = distancenew - height;

            }
        } else {
            if (degree == 45) {
                System.out.println("----------------angulos diferente a 90 graus--------------------------------- ");
                System.out.println("distancia 45 : " + distancenew);
                BigDecimal fit = new BigDecimal("0.03");
                BigDecimal fitx = new BigDecimal("0.03");
                BigDecimal newfit = new BigDecimal("0.0");
                String name = "RectSmall";//"RectTiny";//
                height = 2;
                distancenew = distancenew;
                unknowpointx = 0;
                while (distancenew > 2) {
                    double r = (double) (height) / (double) (distancenew);
                    unknowpointx =  firstpointx + 1;
                    unknowpointy = (int) (firstpointy + 2);
                    int u = firstpointx + 8;
                    System.out.println("firstpointx 45: " + firstpointx);
                     System.out.println("firstpointy 45: " + firstpointy);
                    System.out.println("firstpointx u: " + u);
                    if (firstpointy > lastpointy) {
                        break;
                    }
                    midpointx = (int) ((firstpointx + u) / 2);
                    midpointy = (int) ((firstpointy + unknowpointy) / 2);
                    auxunityy = discretization.multiply(new BigDecimal(midpointy));
                    unityy = (floor.add(auxunityy));
                    unityy = unityy.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                    unityx = (discretization.multiply(new BigDecimal(midpointx)));
                    unityx = unityx.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                    unityx = fitx.add(unityx);
                    newfit = newfit.add(fit);
                    unityynew = unityy.add(newfit);
                    System.out.println("unityynew 45: " + unityynew);
                    String auxiliaryscript = "\n" + "<Block type=" + "\"" + name + "\"" + " material=" + material + " x=\"" + unityx + "\" y=\"" + unityynew + "\" rotation=\"" + "0" + "\" />";
                    script3 = script3 + auxiliaryscript;
                    if (scriptmap.isEmpty()) {
                        keymap = 1;
                    } else {
                        keymap = scriptmap.lastKey() + 1;
                    }
                    count = firstpointy;
                    for (int t = firstpointx; t <= u; ++t) {

                        scenery[count][t] = 'd';
                        scenery[count + 1][t] = 'd';
                        position[count][t] = keymap;
                        position[count + 1][t] = keymap;
                    }

                    scriptmap.put(keymap, auxiliaryscript);
                    Sequence sequence = new Sequence(name, firstpointx, firstpointy, u, unknowpointy, degree);
                    sequenceblock.add(sequence);
                    firstpointx = unknowpointx;
                    firstpointy = unknowpointy;
                    distancenew = distancenew - height;
                }
            } else {
                unknowpointx = 0;
                firstpointx = firstpointx;
                unknowpointx = firstpointx;
                BigDecimal fit = new BigDecimal("0.03");
                BigDecimal fitx = new BigDecimal("0.01");
                BigDecimal newfit = new BigDecimal("0.0");
                String name = "RectSmall";//"RectTiny";//
                height = 2;
                while (distancenew > 2) {
                    double r = (double) (height) / (double) (distancenew);
                    unknowpointx = (int) unknowpointx - 1;
                    unknowpointy = (int) (firstpointy + 2);
                    if (firstpointy > lastpointy) {
                        break;
                    }
                    int u = firstpointx - 8;
                    midpointx = (int) ((firstpointx + u) / 2);
                    midpointy = (int) ((firstpointy + unknowpointy) / 2);
                    // System.out.println("firstpointx 135: " + firstpointx);
                    System.out.println("firstpointy 135: " + firstpointy);
                    System.out.println("lastpointy 135: " + lastpointy);
                    auxunityy = discretization.multiply(new BigDecimal(midpointy));
                    unityy = (floor.add(auxunityy));
                    unityy = unityy.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                    unityx = (discretization.multiply(new BigDecimal(midpointx)));
                    unityx = unityx.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                    unityx = fitx.add(unityx);
                    newfit = newfit.add(fit);
                    unityynew = unityy.add(newfit);
                    unityynew = unityy.add(newfit);
                    System.out.println("unityynew 135: " + unityynew);
                    String auxiliaryscript = "\n" + "<Block type=" + "\"" + name + "\"" + " material=" + material + " x=\"" + unityx + "\" y=\"" + unityynew + "\" rotation=\"" + "0" + "\" />";
                    script3 = script3 + auxiliaryscript;
                    if (scriptmap.isEmpty()) {
                        keymap = 1;
                    } else {
                        keymap = scriptmap.lastKey() + 1;
                    }
                    count = firstpointy;
                    System.out.println("in firstpointx: " + firstpointx);
                    System.out.println("in u: " + u);
                    for (int t = u; t <= firstpointx; ++t) {
                        System.out.println("in firstpointx: " + firstpointx);
                        System.out.println("in u: " + u);
                        scenery[count][t] = 'e';
                        scenery[count + 1][t] = 'e';
                        position[count][t] = keymap;
                        position[count + 1][t] = keymap;
                    }

                    scriptmap.put(keymap, auxiliaryscript);
                    Sequence sequence = new Sequence(name, firstpointx, firstpointy, u, unknowpointy, degree);
                    sequenceblock.add(sequence);
                    firstpointx = unknowpointx;
                    firstpointy = unknowpointy;
                    distancenew = distancenew - height;
                }
            }
        }
    }

    public void createBase() {

        int x1, x2, x3, y1, y2 = 0;
        for (Iterator<Sequence> iterator = sequenceblock.iterator(); iterator.hasNext();) {
            Sequence sequence = (Sequence) iterator.next();
            int beginx = sequence.getBeginx();
            int beginy = sequence.getBeginy();
            int endx = sequence.getEndx();
            int endy = sequence.getEndy();
            String blockname = sequence.getBlockname();
            System.out.println("-----------------inicio da peça-------------------------------- ");
            System.out.println("Blockname: " + blockname);
            System.out.println("ponto inicial x: " + beginx);
            System.out.println("ponto inicial y: " + beginy);
            System.out.println("ponto final x: " + endx);
            System.out.println("ponto final y: " + endy);
            double dgre = sequence.getDegree();
            System.out.println("angulo da peça: " + dgre);
            if ((beginy == 0) || (endy == 0)) {
                System.out.println("no chão nao precisa");
                System.out.println("-----------------fim da peça-------------------------------- ");
            } else {

                System.out.println("entrou aqui " + blockname);
                if (dgre == 90.0) {
                    x1 = beginx;
                    x2 = beginx + 1;
                    y1 = beginy - 1;
                } else {
                    x1 = beginx;
                    x2 = endx;
                    y1 = beginy - 1;
                    check = scenery[beginy + 4][x2];
                    System.out.println("check" + check + "--");

                }

                System.out.println(" x1 " + x1);
                System.out.println(" x2 " + x2);
                System.out.println("y1 " + y1);
                System.out.println("degree " + dgre);
                char check1 = scenery[y1][x1];
                char check2 = scenery[y1][x2];
                System.out.println("check1" + check1 + "--");
                System.out.println("check1 " + (check1 == '\u0000'));
                System.out.println("check2 " + check2);
                System.out.println("check " + check);
               if (check1 == 'e') {
                   break;
               }
                if (check1 != 'd') {
                    System.out.println("entrou aqui pq o check e =: " + check);

                    if (check1 == '\u0000') { // verifica se e vacio 
                        System.out.println("entrou pq nao tem suporte no eixo x1 " + check1);
                        y2 = 0;
                        for (int d = y1; d > 0; d--) {
                            char check3 = scenery[d][x1];
                            if (check3 != '\u0000') {
                                y2 = d + 1;
                                System.out.println("valor do y2 do eixo x1: " + y2);
                            }
                        }
                        int distance = (int) Math.round(Math.sqrt(Math.pow((x1 - x1), 2) + Math.pow((y2 - y1), 2)));
                        System.out.println("Distancia do y1 ate y2 do eixo x1: " + distance);
                        int distancePointsMap = 0;
                        System.out.println("distance " + distance);

                        if (map.containsKey((Integer) distance)) {
                            distancePointsMap = distance;
                        } else {
                            distancePointsMap = map.getClosest((Integer) distance);
                        }
                        ArrayList<Block> list = map.getBlockList(distancePointsMap).getList();
                        int distancenew = distance;
                        int firstpointx = x1;
                        int firstpointy = y2;
                        int lastpointx = x1;
                        int lastpointy = y1;
                        System.out.println("distancenew eixo x1: " + distancenew);
                        System.out.println("firstpointx eixo x1: " + firstpointx);
                        System.out.println("firstpointy eixo x1: " + firstpointy);
                        System.out.println("lastpointx eixo x1: " + lastpointx);
                        System.out.println("lastpointy eixo x1: " + lastpointy);
                        for (Iterator<Block> iterator2 = list.iterator(); iterator2.hasNext();) {
                            Block block = (Block) iterator2.next();
                            String name = block.getName();
                            System.out.println("blockname eixo x1 " + name);
                            int height = block.getHeight();
                            BigDecimal fit = new BigDecimal("0.01");
                            double r = (double) (height) / (double) (distancenew);
                            int unknowpointx = (int) (firstpointx + r * (lastpointx - firstpointx));
                            System.out.println("unknowpointx eixo x1 " + unknowpointx);
                            int unknowpointy = (int) (firstpointy + r * (lastpointy - firstpointy));
                            System.out.println("unknowpointy eixo x1 " + unknowpointy);
                            int midpointx = (int) ((firstpointx + unknowpointx) / 2);
                            int midpointy = (int) ((firstpointy + unknowpointy) / 2);
                            BigDecimal unityx = discretization.multiply(new BigDecimal(midpointx));
                            unityx = unityx.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                            BigDecimal auxunityy = discretization.multiply(new BigDecimal(midpointy));
                            BigDecimal unityy = floor.add(auxunityy.add(fit));
                            unityy = unityy.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                            String auxiliaryscript = "\n" + "<Block type=" + "\"" + name + "\"" + " material=" + "\"" + "ice" + "\"" + " x=\"" + unityx + "\" y=\"" + unityy + "\" rotation=\"" + 90 + "\" />";
                            script3 = script3 + auxiliaryscript;
                            for (int i = firstpointy; i <= unknowpointy + 1; ++i) {
                                int j = firstpointx;
                                scenery[i][j] = 'b';
                                scenery[i][j + 1] = 'b';
                            }
                            firstpointx = unknowpointx;
                            firstpointy = unknowpointy;
                            distancenew = distancenew - height;
                        }
                    } else {
                        System.out.println("tem suporte no eixo x1 a peça " + blockname);
                    }
                    if (check2 == '\u0000') {
                        y2 = 0;
                         /*  if (check1 == 'x'&& dgre != 0){
                          y1 = y1 +1; 
                        }*/
                        for (int d = y1; d > 0; d--) {
                            char check4 = scenery[d][x2];
                            if (check4 != '\u0000') {
                                y2 = d + 1;
                                System.out.println("valor do y2 do eixo x2: " + y2);
                                break;
                            }
                        }
                        int distance = (int) Math.round(Math.sqrt(Math.pow((x2 - x2), 2) + Math.pow((y2 - y1), 2)));
                        int distancePointsMap = 0;
                        System.out.println("Distancia do y1 ate y2 do eixo x2: " + distance);
                        if (map.containsKey((Integer) distance)) {
                            distancePointsMap = distance;
                        } else {
                            distancePointsMap = map.getClosest((Integer) distance);
                        }
                        ArrayList<Block> list = map.getBlockList(distancePointsMap).getList();
                        int distancenew = distance;
                        int firstpointx = x2;
                        int firstpointy = y2;
                        int lastpointx = x2;
                        int lastpointy = y1;
                        System.out.println("distancenew eixo x2: " + distancenew);
                        System.out.println("firstpointx eixo x2: " + firstpointx);
                        System.out.println("firstpointy eixo x2: " + firstpointy);
                        System.out.println("lastpointx eixo x2: " + lastpointx);
                        System.out.println("lastpointy eixo x2: " + lastpointy);
                        for (Iterator<Block> iterator2 = list.iterator(); iterator2.hasNext();) {
                            count = count + 1;
                            Block block = (Block) iterator2.next();
                            String name = block.getName();
                            System.out.println("blockname eixo x2 " + name);
                            int height = block.getHeight();
                            BigDecimal fit = new BigDecimal("0.01");
                            double r = (double) (height) / (double) (distancenew);
                            int unknowpointx = (int) (firstpointx + r * (lastpointx - firstpointx));
                            System.out.println("unknowpointx eixo x2: " + unknowpointx);
                            int unknowpointy = (int) (firstpointy + r * (lastpointy - firstpointy));
                            System.out.println("unknowpointy eixo x2: " + unknowpointy);
                            int midpointx = (int) ((firstpointx + unknowpointx) / 2);
                            int midpointy = (int) ((firstpointy + unknowpointy) / 2);
                            BigDecimal unityx = discretization.multiply(new BigDecimal(midpointx));
                            unityx = unityx.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                            BigDecimal auxunityy = discretization.multiply(new BigDecimal(midpointy));
                            BigDecimal unityy = floor.add(auxunityy.add(fit));
                            unityy = unityy.setScale(2, BigDecimal.ROUND_HALF_EVEN);
                            String auxiliaryscript = "\n" + "<Block type=" + "\"" + name + "\"" + " material=" + "\"" + "ice" + "\"" + " x=\"" + unityx + "\" y=\"" + unityy + "\" rotation=\"" + 90 + "\" />";
                            script3 = script3 + auxiliaryscript;

                            for (int i = firstpointy; i <= unknowpointy + 1; ++i) {
                                int j = firstpointx;
                                System.out.println("entrou aqui : " + i);
                                scenery[i][j] = 'c';
                                scenery[i][j + 1] = 'c';
                            }
                            firstpointx = unknowpointx;
                            firstpointy = unknowpointy;
                            distancenew = distancenew - height;
                        }
                        scenery[y2][x2]='2';
                        scenery[y1][x2]='1';
                         } else {
                        System.out.println("tem suporte no eixo x2 a peça " + blockname);
                    }
                } else {
                    System.out.println("e uma peça de 45 graus " + blockname);
                }
            }
        }
    }

    public void createXml() {
        script2 = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + "\n" + "<Level width=\"2\">" + "\n" + "<Camera x=\"0\" y=\"-1\">" + " minWidth=\"15\" maxWidth=\"17.5\">" + "\n" + "<Birds>" + "\n" + "<Bird type=\"BirdBlue\"/>" + "\n" + "<Bird type=\"BirdBlack\"/>" + "\n" + "<Bird type=\"BirdRed\"/>" + "\n"
                + "</Birds>" + "\n" + "<Slingshot x=\"-8.5\" y=\"-2.5\">" + "\n" + "<GameObjects>";
        script2 = script2 + script3 + "\n" + "</GameObjects>" + "\n" + "</Level>";
        try {
            out = new FileWriter("level-1.xml");
            out.write(script2);
            out.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println("XML Result: " + script2);
    }

    public void printScenery() {
        System.out.printf("\n");
        System.out.printf("matriz posiçao");
        System.out.printf("\n");
        for (int d = 0; d < 70; d++) {
            for (int a = 0; a < 70; a++) {
                System.out.printf("%c\t", scenery[69 - d][a]);
            }
            System.out.printf("\n");

        }
    }
}
