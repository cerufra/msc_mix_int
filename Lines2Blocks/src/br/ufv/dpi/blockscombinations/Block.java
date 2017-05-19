package br.ufv.dpi.blockscombinations;

public class Block {
	private int height;
	private int length;
	private String name;
	
	public Block(int h, int l, String name)
	{
		this.setHeight(h);
		this.setLength(l);
		this.setName(name);
	}

	public void setName(String name) 
	{
		this.name = name;
	}
	
	public String getName() 
	{
		return this.name;
	}

	public int getHeight() 
	{
		return height;
	}

	public void setHeight(int height) 
	{
		this.height = height;
	}

	public int getLength() 
	{
		return length;
	}

	public void setLength(int length) 
	{
		this.length = length;
	}
	

}
