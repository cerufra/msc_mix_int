package br.ufv.dpi.blockscombinations;
public class MainGenerateCombinations {
	
	public static void main(String args[])
	{
		CreateMap create = new CreateMap();
		BlockList list = new BlockList();
        create.search(list, 70);
        create.finish();
   	}

}
