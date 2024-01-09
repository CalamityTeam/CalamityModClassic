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
	public class AncientCrusher : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ancient Crusher");
			//Tooltip.SetDefault("Summons fossil spikes on enemy hits");
		}

		public override void SetDefaults()
		{
			Item.width = 62;
			Item.damage = 52;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 30;
			Item.useTurn = true;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 62;
			Item.value = 150000;
			Item.rare = ItemRarityID.Pink;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Amber, 8);
			recipe.AddIngredient(ItemID.FossilOre, 35);
			recipe.AddIngredient(null, "EssenceofCinder", 3);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("FossilSpike").Type, hit.Damage, hit.Knockback, Main.myPlayer);
		}
	}
}
