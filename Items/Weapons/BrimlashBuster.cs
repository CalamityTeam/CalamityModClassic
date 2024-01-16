using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BrimlashBuster : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/BrimlashBuster");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Brimlash Buster");
		Item.width = 76;  //The width of the .png file in pixels divided by 2.
		Item.damage = 120;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useTime = 25;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		////Tooltip.SetDefault("50% chance to do triple damage on enemy hits");
		Item.useStyle = 1;
		Item.knockBack = 8;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 78;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 3000000;  //Value is calculated in copper coins.
		Item.rare = 9;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Brimlash").Type;
		Item.shootSpeed = 18f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "Brimlash");
        recipe.AddIngredient(null, "CoreofChaos", 3);
        recipe.AddIngredient(ItemID.FragmentSolar, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
    	if (Main.rand.Next(3) == 0)
    	{
    		Item.damage = 360;
    	}
    	else
    	{
    		Item.damage = 120;
    	}
	}
}}
