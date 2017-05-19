package br.ufv.dpi.blockscombinations;

import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;

public class CreateMap {

	private ArrayList<Block> blocksList = new ArrayList<Block>();
	private HashSet<BlockList> closed = new HashSet<BlockList>();
	private FileWriter out;
	public static String filename = "dataset1";
	
	public CreateMap() {

		this.blocksList = FactoryBlock.getAllBlocks();
		
		try 
		{
			out = new FileWriter(filename);
            out.flush();
		} catch (IOException e) 
		{
			System.out.println("Couldn't find file " + filename);
			e.printStackTrace();
		}
	}

	public void search(BlockList list, int xMax)
	{
		int x = list.getX();

		if(x >= xMax || closed.contains(list))
		{
			return;
		}

		if(!list.isEmpty())
		{
			System.out.print(list.getName() + " ");
			System.out.println(x);
			
			try 
			{
				out.write(String.valueOf(x) + ", " + list.getName() + "\n");
			} catch (IOException e) 
			{
				e.printStackTrace();
			}

			closed.add(list);
		}

		for (Block block : blocksList) 
		{
			list.add(block);
			search(list, xMax);
			list.remove(block);
		}
	}
	
	public void finish() {
		try 
		{
			out.close();
		} catch (IOException e) 
		{
			e.printStackTrace();
		}		
	}

}
