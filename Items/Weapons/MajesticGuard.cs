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
	public class MajesticGuard : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Majestic Guard");
			// Tooltip.SetDefault("Has a chance to lower enemy defense by 10 when striking them\nIf enemy defense is 0 or below your attacks will heal you");
		}

		public override void SetDefaults()
		{
			Item.width = 98;
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 22;
			Item.useStyle = 1;
			Item.useTime = 22;
			Item.useTurn = true;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 98;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			if (Main.rand.Next(5) == 0)
			{
				target.defense -= 10;
			}
			if (target.defense <= 0 && target.canGhostHeal)
			{
		    	player.statLife += 3;
		    	player.HealEffect(3);
			}
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AdamantiteSword);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TitaniumSword);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
