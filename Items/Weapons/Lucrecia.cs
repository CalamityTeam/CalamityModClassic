using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Lucrecia : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Lucrecia");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Lucrecia");
		Item.useStyle = 3;
		Item.useTurn = false;
		Item.useAnimation = 25;
		Item.useTime = 25;  //Ranges from 1 to 55.
		Item.width = 58;  //The width of the .png file in pixels divided by 2.
		Item.height = 58;  //The height of the .png file in pixels divided by 2.
		Item.damage = 90;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.knockBack = 8.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.useTurn = true;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.maxStack = 1;
		////Tooltip.SetDefault("Finesse\nStriking an enemy with the blade will make you immune for a short time");
		Item.shoot = Mod.Find<ModProjectile>("DNA").Type;
		Item.shootSpeed = 32f;
		Item.value = 3000000;  //Value is calculated in copper coins.
		Item.rare = 8;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CoreofCalamity");
		recipe.AddIngredient(null, "BarofLife", 8);
		recipe.AddIngredient(ItemID.SoulofLight, 5);
		recipe.AddIngredient(ItemID.SoulofNight, 5);
        recipe.AddTile(TileID.MythrilAnvil);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 234);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		player.immune = true;
		player.immuneTime = 20;
	}
}}
