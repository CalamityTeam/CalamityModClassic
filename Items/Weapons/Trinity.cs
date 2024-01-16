using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Trinity : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Trinity");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Trinity");
		Item.width = 44;  //The width of the .png file in pixels divided by 2.
		Item.damage = 57;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useTime = 25;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 4.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 46;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 110000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
		Item.shoot = 125;
		Item.shootSpeed = 11f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	switch (Main.rand.Next(6))
		{
    		case 1: type = 125; break;
    		case 2: type = 123; break;
    		case 3: type = 121; break;
    		default: break;
		}
    	for (int projectiles = 0; projectiles <= 3; projectiles++)
    	{
    		float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
       		Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, (int)((double)damage * 0.65f), knockback, Main.myPlayer);
    	}
    	return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VerstaltiteBar", 9);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 73);
        }
    }
}}
