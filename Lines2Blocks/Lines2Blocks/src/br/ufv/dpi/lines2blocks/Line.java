package br.ufv.dpi.lines2blocks;

public class Line {

    private Point p1;
    private Point p2;
    private String material;

    public Line(Point p1, Point p2, String m) {
        setP1(p1);
        setP2(p2);
        setMaterial(m);
    }

    public Point getP2() {
        return p2;
    }

    public void setP2(Point p2) {
        this.p2 = p2;
    }

    public Point getP1() {
        return p1;
    }

    public void setP1(Point p1) {
        this.p1 = p1;
    }

    public String getMaterial() {
        return material;
    }

    public void setMaterial(String material) {
        this.material = material;
    }

}
