using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SporeKnife : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Spore Knife");
		//Tooltip.SetDefault("Enemies release spore clouds on death");
	}

	public override void SetDefaults()
	{
		Item.useStyle = ItemUseStyleID.Thrust;
		Item.useTurn = false;
		Item.useAnimation = 12;
		Item.useTime = 12;  //Ranges from 1 to 55.
		Item.width = 28;  //The width of the .png file in pixels divided by 2.
		Item.height = 28;  //The height of the .png file in pixels divided by 2.
		Item.damage = 33;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.knockBack = 5.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.useTurn = true;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.maxStack = 1;
		Item.value = 45000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Orange;  //Ranges from 1 to 11.
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.JungleSpores, 10);
		recipe.AddIngredient(ItemID.Stinger, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Grass);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	if (target.life <= 0)
    	{
    		Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Main.rand.Next(569, 572), hit.Damage, hit.Knockback, Main.myPlayer);
    	}
	}
}}
