using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Lazhar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Lazhar");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Lazhar");
        Item.damage = 115;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 4;
        Item.width = 42;
        Item.height = 20;
        ////Tooltip.SetDefault("Fires a bouncing heat ray that explodes on enemy hits");
        Item.useTime = 7;
        Item.useAnimation = 7;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5f;
        Item.value = 1050000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item12;
        Item.autoReuse = true;
        Item.shootSpeed = 15f;
        Item.shoot = Mod.Find<ModProjectile>("SolarBeam2").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
	    Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HeatRay);
        recipe.AddIngredient(ItemID.FragmentSolar, 10);
        recipe.AddIngredient(ItemID.ChlorophyteBar, 6);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}