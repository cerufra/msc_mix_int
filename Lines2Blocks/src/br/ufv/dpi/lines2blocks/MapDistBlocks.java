package br.ufv.dpi.lines2blocks;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Scanner;

import br.ufv.dpi.blockscombinations.BlockList;

public class MapDistBlocks {

	//private HashMap<Integer, ArrayList<BlockList > > map = new HashMap<Integer, ArrayList<BlockList > >();
	private HashMap<Integer, BlockList> map = new HashMap<Integer, BlockList>();

	MapDistBlocks(String filename) 
	{
		Scanner scanner;
		try 
		{
			File file = new File(filename);
			scanner = new Scanner(file);
			while(scanner.hasNextLine())
			{
				String line[] = scanner.nextLine().split(",");
				Integer length = Integer.parseInt(line[0]);
				String blockNames = line[1];
				String[] names = blockNames.split(" ");

				BlockList blockList = new BlockList();
				for (int i = 0; i < names.length; i++) 
				{
					blockList.add(names[i]);
				}

				if(!map.containsKey(length)) 
				{
					//map.put(length, new ArrayList<>());
					//map.get(length).add(blockList);
					map.put(length, blockList);
				}
				else
				{
					if(map.get(length).getList().size() > blockList.getList().size())
					{
						map.put(length, blockList);
					}
					//map.get(length).add(blockList);
				}			
			}
		} catch (FileNotFoundException e) 
		{
			e.printStackTrace();
		}
	}
	
	public Boolean containsKey(Integer i) 
	{
		if(map.containsKey(i))
		{
			return true;
		}
		return false;
	}
	
	public Integer getClosest(Integer i) 
	{
		if(map.containsKey(i))
		{
			return i;
		}
		
		Iterator<Integer> iter = map.keySet().iterator();
		Integer closest = iter.next();
		while(iter.hasNext() && closest < i)
		{
			closest = iter.next();
		}
		
		return closest;
	}
	
	public BlockList getBlockList(Integer i)
	{
		//return map.get(i).get(0);
		return map.get(i);
	}

	/*
	 * This method provides an example of how to iterate through the map.
	 * In this example we simply print the blocks' length and their combination.
	 */
	/*  public MapDistBlocks(HashMap mapi)
	{
		this.setMap(mapi);

	}

        public void setMap(HashMap map) 
	{
		this.map = map;
	}
        public HashMap getMap() 
	{
		return this.map;
	}*/

	public void printMap() {
		Iterator<Integer> iter = map.keySet().iterator();

		while(iter.hasNext()) 
		{
			Integer i = iter.next();
			System.out.println("\n\nLength: " + i);
			System.out.println(map.get(i).getName());
			/*
			ArrayList<BlockList> arrayList = map.get(i);
			for (Iterator<BlockList> iterator = arrayList.iterator(); iterator.hasNext();) 
			{
				BlockList blockList = (BlockList) iterator.next();
				System.out.println(blockList.getName());
			}
			*/
		}

	}
}

