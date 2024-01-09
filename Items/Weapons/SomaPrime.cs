using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SomaPrime : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("THIS WEAPON IS A REFERENCE TO WARFRAME OH MAH GAWD");
	}

    public override void SetDefaults()
    {
        Item.damage = 550;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 56;
        Item.height = 22;
        Item.crit += 50;
        Item.useTime = 1;
        Item.useAnimation = 3;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2f;
        Item.value = 10000000;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("SlashRound").Type;
        Item.shootSpeed = 30f;
        Item.useAmmo = 97;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(255, 0, 255);
            }
        }
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "ShadowspecBar", 5);
		recipe.AddIngredient(null, "P90");
		recipe.AddIngredient(null, "Minigun");
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
	    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("SlashRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 50)
    		return false;
    	return true;
    }
}}