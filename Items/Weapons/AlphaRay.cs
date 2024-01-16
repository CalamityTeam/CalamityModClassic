using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AlphaRay : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/AlphaRay");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Alpha Ray");
        Item.damage = 150;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 6;
        Item.width = 78;
        Item.height = 70;
        ////Tooltip.SetDefault("Disintegrates everything");
        Item.useTime = 3;
        Item.useAnimation = 3;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 1.5f;
        Item.value = 10000000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item33;
        Item.autoReuse = true;
        Item.shootSpeed = 6f;
        Item.shoot = Mod.Find<ModProjectile>("ParticleBeamofDoom").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num117 = 0.314159274f;
		int num118 = 3;
		Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
		vector7.Normalize();
		vector7 *= 80f;
		bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
		for (int num119 = 0; num119 < num118; num119++)
		{
			float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
			Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
			if (!flag11)
			{
				value9 -= vector7;
			}
			Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
			int laser = Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, 440, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
			Main.projectile[laser].timeLeft = 120;
        	Main.projectile[laser].velocity.X *= 2f;
        	Main.projectile[laser].velocity.Y *= 2f;
        	Main.projectile[laser].tileCollide = false;
		}
		return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GalacticaSingularity", 5);
        recipe.AddIngredient(null, "Wingman", 2);
        recipe.AddIngredient(null, "Genisis");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}