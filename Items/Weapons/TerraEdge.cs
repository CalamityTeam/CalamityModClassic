using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TerraEdge : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Terra Edge");
		//Tooltip.SetDefault("Heals the player on enemy hits");
	}

	public override void SetDefaults()
	{
		Item.width = 58;  //The width of the .png file in pixels divided by 2.
		Item.damage = 87;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 17;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 17;  //Ranges from 1 to 55.
		Item.useTurn = true;
		Item.knockBack = 6.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item60;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 58;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1060000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TerraEdgeBeam").Type;
		Item.shootSpeed = 12f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TrueBloodyEdge");
		recipe.AddIngredient(ItemID.TrueExcalibur);
        recipe.AddTile(TileID.MythrilAnvil);	
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.TrueNightsEdge);
		recipe.AddIngredient(ItemID.TrueExcalibur);
        recipe.AddTile(TileID.MythrilAnvil);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GreenFairy);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if (target.type == NPCID.TargetDummy)
		{
			return;
		}
    	int healAmount = (Main.rand.Next(3) + 2);
    	player.statLife += healAmount;
    	player.HealEffect(healAmount);
	}
}}
