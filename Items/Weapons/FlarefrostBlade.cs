using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class FlarefrostBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/FlarefrostBlade");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Flarefrost Blade");
		Item.width = 66;  //The width of the .png file in pixels divided by 2.
		Item.damage = 63;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 28;
		Item.useTime = 28;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 6.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 66;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 550000;  //Value is calculated in copper coins.
		Item.rare = 6;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Flarefrost").Type;
		Item.shootSpeed = 9f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CryoBar", 8);
		recipe.AddIngredient(ItemID.HellstoneBar, 8);
		recipe.AddIngredient(ItemID.HallowedBar, 4);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
    	int dustChoice = Main.rand.Next(2);
    	if (dustChoice == 0)
    	{
    		dustChoice = 67;
    	}
    	else
    	{
    		dustChoice = 6;
    	}
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, dustChoice);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 200);
		target.AddBuff(BuffID.Frostburn, 200);
	}
}}
