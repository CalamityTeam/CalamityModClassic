using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TrueForbiddenOathblade : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("True Forbidden Oathblade");
			//Tooltip.SetDefault("Sword of an ancient demon god");
		}

	public override void SetDefaults()
	{
		Item.width = 78;  //The width of the .png file in pixels divided by 2.
		Item.damage = 84;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 23;
		Item.useTime = 23;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 78;  //The height of the .png file in pixels divided by 2.
		Item.value = 1000000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Oathblade").Type;
		Item.shootSpeed = 3f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int numProj = 2;
	    float rotation = MathHelper.ToRadians(8);
	    for (int i = 0; i < numProj + 1; i++)
	    {
	    	Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
	        Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    }
	    return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "ForbiddenOathblade");
		recipe.AddIngredient(null, "EssenceofChaos", 3);
		recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.ShadowbeamStaff);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.ShadowFlame, 300);
		target.AddBuff(BuffID.OnFire, 300);
	}
}}
