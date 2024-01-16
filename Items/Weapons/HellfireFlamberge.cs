using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class HellfireFlamberge : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/HellfireFlamberge");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Hellfire Flamberge");
		Item.width = 50;  //The width of the .png file in pixels divided by 2.
		Item.damage = 79;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.useAnimation = 27;
		Item.useStyle = 1;
		Item.useTime = 27;
		Item.useTurn = true;
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 50;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 415000;  //Value is calculated in copper coins.
		Item.rare = 7;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("ChaosFlameSmall").Type;
		Item.shootSpeed = 16f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	float SpeedA = velocity.X;
   		float SpeedB = velocity.Y;
        int num6 = Main.rand.Next(3, 5);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = velocity.X;
            float num8 = velocity.Y;
            float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
    		switch (Main.rand.Next(3))
			{
    			case 0: type = Mod.Find<ModProjectile>("ChaosFlameSmall").Type; break;
    			case 1: type = Mod.Find<ModProjectile>("ChaosFlameMedium").Type; break;
    			case 2: type = Mod.Find<ModProjectile>("ChaosFlameLarge").Type; break;
    			default: break;
			}
            Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, (int)((double)damage * 0.75), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CruptixBar", 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 174);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 300);
	}
}}
