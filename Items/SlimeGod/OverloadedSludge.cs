using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Items.SlimeGod
{
	public class OverloadedSludge : ModItem
	{
		public override void SetDefaults()
		{
			//DisplayName.SetDefault("Overloaded Sludge");
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			//AddTooltip("It looks corrupted");
			//AddTooltip2("Can be used before hardmode");
			Item.rare = 5;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SlimeGod").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SlimeCrown);
			recipe.AddIngredient(ItemID.Gel, 100);
			recipe.AddIngredient(ItemID.EbonstoneBlock, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SlimeCrown);
			recipe.AddIngredient(ItemID.Gel, 100);
			recipe.AddIngredient(ItemID.CrimstoneBlock, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "PurifiedGel", 20);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}