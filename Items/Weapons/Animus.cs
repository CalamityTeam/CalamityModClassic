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
	public class Animus : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Animus");
		}

		public override void SetDefaults()
		{
			Item.width = 84;
			Item.damage = 4000;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 11;
			Item.useStyle = 1;
			Item.useTime = 11;
			Item.useTurn = true;
			Item.knockBack = 20f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 90;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "BladeofEnmity");
			recipe.AddIngredient(null, "ShadowspecBar", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 5000);
			int damageRan = Main.rand.Next(195); //0 to 195
			if (damageRan >= 50 && damageRan <= 99) //25%
			{
				Item.damage = 6000;
			}
			else if (damageRan >= 100 && damageRan <= 139) //20%
			{
				Item.damage = 9000;
			}
			else if (damageRan >= 140 && damageRan <= 169) //15%
			{
				Item.damage = 15000;
			}
			else if (damageRan >= 170 && damageRan <= 189) //10%
			{
				Item.damage = 30000;
			}
			else if (damageRan >= 190 && damageRan <= 194) //5%
			{
				Item.damage = 50000;
			}
			else
			{
				Item.damage = 4000;
			}
		}
	}
}
