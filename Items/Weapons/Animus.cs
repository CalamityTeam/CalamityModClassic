using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class Animus : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Animus");
		}

		public override void SetDefaults()
		{
			Item.width = 82;
			Item.damage = 1000;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 11;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 11;
			Item.useTurn = true;
			Item.knockBack = 20f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 84;
			Item.value = 8000000;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, 0, 255);
	            }
	        }
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
				Item.damage = 2000;
			}
			else if (damageRan >= 100 && damageRan <= 139) //20%
			{
				Item.damage = 4000;
			}
			else if (damageRan >= 140 && damageRan <= 169) //15%
			{
				Item.damage = 8000;
			}
			else if (damageRan >= 170 && damageRan <= 189) //10%
			{
				Item.damage = 16000;
			}
			else if (damageRan >= 190 && damageRan <= 194) //5%
			{
				Item.damage = 36000;
			}
			else
			{
				Item.damage = 1000;
			}
		}
	}
}
