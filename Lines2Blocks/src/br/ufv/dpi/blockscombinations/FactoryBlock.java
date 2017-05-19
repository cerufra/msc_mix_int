package br.ufv.dpi.blockscombinations;

import java.util.ArrayList;

public class FactoryBlock {
	
	//TODO: Change names to static strings.
	public static String rectTiny = "RectTiny"; 
        public static String rectSmall = "RectSmall"; 
        public static String squareTiny = "SquareTiny"; 
        public static String rectBig = "RectBig"; 
        public static String rectMedium = "RectMedium"; 

	public static Block create(String name) 
	{
		Block b;
		
		if(name.equals(rectTiny))
		{
			b = new Block(4, 2, rectTiny);
		}  else if(name.equals(rectSmall))
		{
			b = new Block(9, 2, rectSmall);
		} else if(name.equals(squareTiny))
		{
			b = new Block(2, 2, squareTiny);
		} else if(name.equals(rectBig))
		{
			b = new Block(21, 2, rectBig);
		} else if(name.equals(rectMedium))
		{
			b = new Block(17, 2, rectMedium);
		} else 
		{
			return null;
		}
		
		return b;
	}
	
	public static ArrayList<Block> getAllBlocks()
	{
		Block rectTiny = new Block(4, 2, FactoryBlock.rectTiny);
		Block rectSmall = new Block(9, 2, FactoryBlock.rectSmall);
		Block squareTiny = new Block(2, 2, FactoryBlock.squareTiny);
		Block rectBig = new Block(21, 2, FactoryBlock.rectBig);
		Block rectMedium = new Block(17, 2, FactoryBlock.rectMedium);	
		
		ArrayList<Block> blocksList = new ArrayList<Block>();
		
		blocksList.add(rectTiny);
		blocksList.add(rectSmall);
		blocksList.add(squareTiny);
		blocksList.add(rectBig);
		blocksList.add(rectMedium);	
		
		return blocksList;
	}
	
}
