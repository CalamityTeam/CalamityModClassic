using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Providence
{
	public class ProfanedCore : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Profaned Core");
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			////Tooltip.SetDefault("The core of the unholy flame");
			Item.rare = 10;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("ProvidenceSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneHallow || player.ZoneUnderworldHeight;
		}
	}
}