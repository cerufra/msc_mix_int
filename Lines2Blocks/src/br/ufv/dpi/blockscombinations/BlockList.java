package br.ufv.dpi.blockscombinations;

import java.util.ArrayList;
import java.util.PriorityQueue;

public class BlockList {
	private ArrayList<Block> list = new ArrayList<Block>();
	private PriorityQueue<String> blockNames = new PriorityQueue<String>();

	public boolean constains(Block b)
	{
		return blockNames.contains(b.getName());
	}

	public void add(Block b)
	{
		list.add(b);
		blockNames.add(b.getName());
	}

	public void add(String name)
	{
		Block b = FactoryBlock.create(name);
		if(b != null)
		{
			list.add(b);
			blockNames.add(b.getName());
		}
	}

	public boolean remove(Block b)
	{
		blockNames.remove(b.getName());
		return list.remove(b);
	}
	
	public ArrayList<Block> getList()
	{
		return list;
	}

	public String getName()
	{
		String name = "";
		for (String s : blockNames) 
		{
			name += s + " ";
		}
		return name;
	}

	public int getX() {
		int x = 0;
		for (Block block : list)
		{
			x += block.getHeight();
		}
		return x;			
	}

	public boolean isEmpty() 
	{
		return list.isEmpty();
	}

	@Override
	public boolean equals(Object obj) 
	{
		if(obj != null && obj instanceof BlockList) 
		{
			BlockList l = (BlockList)obj;
			if(l.blockNames.size() != this.blockNames.size())
			{
				return false;
			}

			String [] names1 = (String []) l.blockNames.toArray();
			String [] names2 = (String []) this.blockNames.toArray();

			for (int i = 0; i < names2.length; i++) 
			{
				if(names1[i].equals(names2[i]))
				{
					return false;
				}
			}

			return true;        
		}
		return false;
	}

	@Override
	public int hashCode() 
	{
		return (this.getName()).hashCode();
	}


}
