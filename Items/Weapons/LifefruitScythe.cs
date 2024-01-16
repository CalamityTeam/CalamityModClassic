using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class LifefruitScythe : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/LifefruitScythe");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Lifefruit Scythe");
		Item.width = 66;  //The width of the .png file in pixels divided by 2.
		Item.damage = 75;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 28;
		Item.useStyle = 1;
		Item.useTime = 28;
		Item.useTurn = true;
		Item.knockBack = 7.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item71;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 68;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		////Tooltip.SetDefault("Heals the player when attacking enemies");
		Item.value = 350000;  //Value is calculated in copper coins.
		Item.rare = 8;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("LifeScythe").Type;
		Item.shootSpeed = 9f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "UeliaceBar", 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(4) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 75);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	player.statLife += 3;
    	player.HealEffect(3);
		target.AddBuff(BuffID.OnFire, 200);
		target.AddBuff(BuffID.CursedInferno, 200);
	}
}}
