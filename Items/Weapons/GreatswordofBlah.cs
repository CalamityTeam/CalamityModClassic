using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class GreatswordofBlah : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Greatsword of Blah");
			/* Tooltip.SetDefault("A pale white sword from a forgotten land\n" +
			                   "You can hear faint yet comforting whispers emanating from the blade\n" +
					            "'No matter where you may be you are never alone.\n" +
					            "I shall always be at your side, my lord'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 110;
			Item.damage = 280;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.useTime = 18;
			Item.useTurn = true;
			Item.knockBack = 7f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 110;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("JudgementBlah").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GreatswordofJudgement");
			recipe.AddIngredient(null, "NightmareFuel", 10);
        	recipe.AddIngredient(null, "EndothermicEnergy", 10);
			recipe.AddIngredient(null, "DarksunFragment", 10);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	}
}
