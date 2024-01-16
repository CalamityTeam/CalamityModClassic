using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Genisis : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Genisis");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Genisis");
        Item.damage = 136;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 5;
        Item.width = 66;
        Item.height = 28;
        ////Tooltip.SetDefault("Shoots a big ol' death beam and a volley of laser blasts\nThe beam splits in two upon death");
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
        Item.shoot = Mod.Find<ModProjectile>("BigBeamofDeath").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	Projectile.NewProjectile(source, position, velocity,type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        int num6 = 3;
        float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
        for (int index = 0; index < num6; ++index)
        {
            int projectile = Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), 440, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
            Main.projectile[projectile].timeLeft = 120;
        	Main.projectile[projectile].velocity.X *= 1.05f;
        	Main.projectile[projectile].velocity.Y *= 1.05f;
        }
        return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LaserMachinegun);
        recipe.AddIngredient(ItemID.LunarBar, 10);
        recipe.AddIngredient(null, "BarofLife", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}