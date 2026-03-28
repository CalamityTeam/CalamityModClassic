using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.SlimeGod
{
	public class OverloadedSludge : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Overloaded Sludge");
			/* Tooltip.SetDefault("It looks corrupted\n" +
                "Summons the Slime God"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = 4;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodCore").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGod").Type) && 
                !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodSplit").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRun").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRunSplit").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SlimeGod").Type);
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SlimeGodRun").Type);
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SlimeGodCore").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "EbonianGel", 25);
			recipe.AddIngredient(ItemID.EbonstoneBlock, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "EbonianGel", 25);
			recipe.AddIngredient(ItemID.CrimstoneBlock, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "PurifiedGel", 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}