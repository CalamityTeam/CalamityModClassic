using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class OmegaBiomeBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/OmegaBiomeBlade");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Omega Biome Blade");
		Item.width = 58;  //The width of the .png file in pixels divided by 2.
		Item.damage = 140;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 18;
		Item.useTime = 18;  //Ranges from 1 to 55.
		////Tooltip.SetDefault("Fires different homing projectiles based on what biome you're in\nProjectiles also change based on moon events");
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 8;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 58;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1200000;  //Value is calculated in copper coins.
		Item.rare = 9;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("OmegaBiomeOrb").Type;
		Item.shootSpeed = 15f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int projectiles = 0; projectiles <= 2; projectiles++)
		{
    		Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("OmegaBiomeOrb").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		}
    	return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TrueBiomeBlade");
		recipe.AddIngredient(null, "CoreofCalamity");
		recipe.AddIngredient(null, "BarofLife", 3);
		recipe.AddIngredient(null, "GalacticaSingularity", 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 0);
        }
    }
}}
