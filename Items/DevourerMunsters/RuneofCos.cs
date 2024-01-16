using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.DevourerMunsters
{
	public class RuneofCos : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Rune of Kos");
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			////Tooltip.SetDefault("Used to seal the sentinels of the cosmic devourer\nWhen used in certain areas of the world it will unleash them\nNot consumable");
			Item.rare = 10;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			Item.shoot = Mod.Find<ModProjectile>("VoidSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneSkyHeight || player.ZoneUnderworldHeight || player.ZoneDungeon && 
				(!NPC.AnyNPCs(Mod.Find<ModNPC>("StormWeaverHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("StormWeaverHeadNaked").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("CeaselessVoid").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("CosmicWraith").Type));
		}
	}
}